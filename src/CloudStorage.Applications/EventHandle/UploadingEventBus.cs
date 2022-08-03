using CloudStoage.Domain;
using CloudStoage.Domain.Etos;
using CloudStorage.Applications.Helpers;
using CloudStorage.Domain.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Diagnostics;
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
    private bool succee = false;
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
            var file = File.OpenRead(x.FilePath);
            UploadingList.Add(new UploadingDto
            {
                Id = x.Id,
                FileName = x.FileName,
                Length = file.Length,
                Stats = UpdateStats.BeUploading
            });
            file.Close();
        });

        // 接受服务器文件上传指令
        connection.On<bool>("file", (x) =>
        {
            succee = true;
        });

        int size = (1024 * 30);
        foreach (var item in eventData)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(item.FilePath);

                item.Length = fileStream.Length;

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
                var sw = Stopwatch.StartNew();

                // 保存上一次计算上传速率时间
                var now = DateTime.Now;
                // 保存上次计算上传速率大小
                int rate = 0;

                while ((len = await fileStream.ReadAsync(b)) != 0)
                {
                    await channel.Writer.WriteAsync(b);
                    await channel.Writer.WaitToWriteAsync();
                    bytesTransferred += len;
                    for (int i = 0; i < 5; i++)
                    {
                        if (succee)
                        {
                            succee = false;
                            break;
                        }
                        else
                        {
                            await Task.Delay(1);
                        }
                    }
                    rate += len;
                    // 固定计算每秒上传速率 
                    if (DateTime.Now > now.AddSeconds(1))
                    {
                        await UploadingSizeEvent(item.Id, bytesTransferred, rate);
                        rate = 0;
                        now = DateTime.Now;
                    }
                }

                await UploadingSizeEvent(item.Id, bytesTransferred, rate);
                sw.Stop();

                // 传输完成结束通道
                channel.Writer.Complete();
                await DistributedEventBus.PublishAsync("Storages", "上传文件成功");

                await UploadingSizeEvent(item.Id, succeed: true);
            }
            finally
            {
                fileStream?.Close();
            }
        }
    }

    private async Task UploadingSizeEvent(Guid id, int BytesTransferred = 0, int rate = 0, bool succeed = false)
    {
        foreach (var d in UploadingList)
        {
            if (d.Id == id)
            {
                if (succeed)
                {
                    d.Stats = UpdateStats.Succeed;
                    d.Rate = rate;
                    await KeyLocalEventBus.PublishAsync(KeyLoadNames.UploadingListName, d);
                    return;
                }
                d.UploadingSize = BytesTransferred;
                d.Rate = rate;
                await KeyLocalEventBus.PublishAsync(KeyLoadNames.UploadingListName, d);
                return;
            }
        }
    }

}
