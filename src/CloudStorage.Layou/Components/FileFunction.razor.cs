
using Token.EventBus;

namespace CloudStorage.Layou.Components;

partial class FileFunction
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Parameter]
    public Guid StorageId { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }

    [Inject]
    public IKeyLocalEventBus<bool> DistributedEventBus { get; set; }

    private async void DeleteFileAsync()
    {
        await StorageApi.DeleteStorageAsync(StorageId);

        await DistributedEventBus.PublishAsync("HasFybctuib", false);
    }
}
