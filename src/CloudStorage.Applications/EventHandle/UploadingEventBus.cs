using CloudStoage.Domain;
using CloudStoage.Domain.Etos;
using CloudStorage.Applications.Helpers;
using CloudStorage.Domain.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;
using System.Net.Http.Handlers;
using Token.EventBus;
using Token.EventBus.Handlers;
using Token.Module.Dependencys;

namespace CloudStorage.Applications.EventHandle;

public class UploadingEventBus : ILocalEventHandler<List<UploadingEto>>, ISingletonDependency
{
    public BlockingCollection<UploadingDto> UploadingList { get; set; } = new BlockingCollection<UploadingDto>();

    private readonly IKeyLocalEventBus<bool> KeyLocalEventBus;

    private readonly HtttpClientHelper _htttpClientHelper;

    public UploadingEventBus(HtttpClientHelper htttpClientHelper, IKeyLocalEventBus<bool> keyLocalEventBus)
    {
        _htttpClientHelper = htttpClientHelper;
        KeyLocalEventBus = keyLocalEventBus;
    }

    public async Task HandleEventAsync(List<UploadingEto> eventData)
    {
        foreach (var item in eventData)
        {
            UploadingList.Add(new UploadingDto
            {
                Id = item.Id,
                FileName = item.FileName,
                Length = item.Length ?? 0,
                Stats = UpdateStats.BeUploading
            });

            await _htttpClientHelper.UpdateRand(item, HttpProgressEvent);
        }

    }

    private async void HttpProgressEvent(object data, HttpProgressEventArgs eventArgs)
    {
        var http = data as HttpRequestMessage;
        var value = http.Headers.GetValues("id").FirstOrDefault();

        if (!string.IsNullOrEmpty(value))
        {
            var id = Guid.Parse(value);

            foreach (var d in UploadingList)
            {
                if (d.Id == id)
                {
                    d.UploadingSize = eventArgs.BytesTransferred;
                    await KeyLocalEventBus.PublishAsync(KeyLoadNames.UploadingListName, true);
                    return;
                }
            }
        }
    }
}
