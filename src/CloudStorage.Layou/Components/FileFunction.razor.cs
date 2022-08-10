
using CloudStoage.Domain.HttpModule.Result;
using Token.EventBus;

namespace CloudStorage.Layou.Components;

partial class FileFunction
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Parameter]
    public StorageDto Storage { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }

    [Inject]
    public IKeyLocalEventBus<bool> DistributedEventBus { get; set; }

    private async void DeleteFileAsync()
    {
        await StorageApi.DeleteStorageAsync(Storage.Id);

        await DistributedEventBus.PublishAsync("HasFybctuib", false);
    }
}
