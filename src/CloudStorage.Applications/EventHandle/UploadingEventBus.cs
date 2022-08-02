using CloudStoage.Domain;
using CloudStoage.Domain.Etos;
using CloudStorage.Applications.Helpers;
using CloudStorage.Domain.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
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

    private readonly IKeyLocalEventBus<UploadingDto> KeyLocalEventBus;
    private readonly IKeyLocalEventBus<string> DistributedEventBus;
    private readonly IConfiguration _configuration;
    private readonly TokenManage token;

    private HubConnection connection;

    public UploadingEventBus(IKeyLocalEventBus<UploadingDto> keyLocalEventBus, TokenManage token, IKeyLocalEventBus<string> distributedEventBus, IConfiguration configuration)
    {
        KeyLocalEventBus = keyLocalEventBus;
        this.token = token;
        DistributedEventBus = distributedEventBus;
        _configuration = configuration;
    }

    public async Task HandleEventAsync(List<UploadingEto> eventData)
    {
        if (connection == null || connection.State != HubConnectionState.Connected)
        {
            connection = new HubConnectionBuilder()
                .WithUrl(_configuration["HostApi"] + "/file-stream", option =>
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
            var channel = Channel.CreateBounded<byte[]>(length + 1);

            // 建立传输通道
            await connection.SendAsync("FileStreamSaveAsync", channel.Reader, JsonConvert.SerializeObject(new
            {
                item.StorageId,
                item.FileName,
                item.Length
            }));

            var bytesTransferred = 0;

            // 定义下载缓存
            var b = new byte[size > item.Length ? item.Length ?? 0 : size];
            int len;
            while ((len = await item.Stream.ReadAsync(b)) != 0)
            {

                await channel.Writer.WriteAsync(b);
                bytesTransferred += len;
                await UploadingSizeEvent(item.Id, bytesTransferred);
            }

            // 传输完成结束通道
            channel.Writer.Complete();
            await DistributedEventBus.PublishAsync("Storages", "上传文件成功");

            await UploadingSizeEvent(item.Id, succeed: true);
        }
    }

    private async Task UploadingSizeEvent(Guid id, int BytesTransferred = 0, bool succeed = false)
    {
        foreach (var d in UploadingList)
        {
            if (d.Id == id)
            {
                if (succeed)
                {
                    d.Stats = UpdateStats.Succeed;
                    await KeyLocalEventBus.PublishAsync(KeyLoadNames.UploadingListName, d);
                    return;
                }
                d.UploadingSize = BytesTransferred;
                await KeyLocalEventBus.PublishAsync(KeyLoadNames.UploadingListName, d);
                return;
            }
        }
    }

}
