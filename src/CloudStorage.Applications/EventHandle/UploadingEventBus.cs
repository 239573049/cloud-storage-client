using CloudStoage.Domain;
using CloudStoage.Domain.Etos;
using CloudStorage.Applications.Helpers;
using CloudStorage.Domain.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Token.EventBus;
using Token.EventBus.Handlers;
using Token.Module.Dependencys;

namespace CloudStorage.Applications.EventHandle;

public class UploadingEventBus : ILocalEventHandler<List<UploadingEto>>, ISingletonDependency
{
    public BlockingCollection<UploadingDto> UploadingList { get; set; } = new BlockingCollection<UploadingDto>();

    private readonly IKeyLocalEventBus<Tuple<long, Guid>> KeyLocalEventBus;

    private readonly TokenManage token;

    private readonly HtttpClientHelper _htttpClientHelper;

    private HubConnection connection;

    public UploadingEventBus(HtttpClientHelper htttpClientHelper, IKeyLocalEventBus<Tuple<long, Guid>> keyLocalEventBus, TokenManage token)
    {
        _htttpClientHelper = htttpClientHelper;
        KeyLocalEventBus = keyLocalEventBus;
        this.token = token;
    }

    public async Task HandleEventAsync(List<UploadingEto> eventData)
    {
        if (connection == null || connection.State != HubConnectionState.Connected)
        {
            connection = new HubConnectionBuilder()
                .WithUrl(Constant.Api + "/file-stream", option =>
                {
                    option.AccessTokenProvider = () => Task.FromResult(token.Token);
                })
                .AddJsonProtocol()
                .Build();

            await connection.StartAsync();
        }

        eventData.ForEach(x =>
        {
            UploadingList.Add(new UploadingDto
            {
                Id = x.Id,
                FileName = x.FileName,
                Length = x.Length ?? 0,
                Stats = UpdateStats.BeUploading
            });
        });

        int size = (1024 * 10);
        foreach (var item in eventData)
        {
            int length = (int)(item.Length / size);
            var channel = Channel.CreateBounded<byte[]>(length);

            await connection.SendAsync("FileStreamSaveAsync", channel.Reader, JsonConvert.SerializeObject(new
            {
                StorageId = item.StorageId,
                FileName = item.FileName,
                Length = item.Length
            }));

            var bytesTransferred = 0;
            for (int k = 0; k < length; k++)
            {
                var b = new byte[size];
                await item.Stream.ReadAsync(b);
                await channel.Writer.WriteAsync(b);
                bytesTransferred += b.Length;
                if (k != 0)
                {
                    await UploadingSizeEvent(item.Id, bytesTransferred);
                }
            }
            channel.Writer.Complete();
        }
    }

    private async Task UploadingSizeEvent(Guid id, int BytesTransferred)
    {
        foreach (var d in UploadingList)
        {
            if (d.Id == id)
            {
                d.UploadingSize = BytesTransferred;
                await KeyLocalEventBus.PublishAsync(KeyLoadNames.UploadingListName, new Tuple<long, Guid>(BytesTransferred, id));
                return;
            }
        }
    }

}
