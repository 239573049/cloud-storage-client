using CloudStorage.Layou.Pages;
using Token.EventBus;

namespace CloudStorage.Layou.Components;

partial class CreateFolder
{
    /// <summary>
    /// 是否显示
    /// </summary>
    [Parameter]
    public bool Dialog { get; set; }

    public string? Name = string.Empty;

    [Parameter]
    public EventCallback<bool> DialogChanged { get; set; }

    [Parameter]
    public Guid? StorageId { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }
    [Inject]
    public IKeyLocalEventBus<string> DistributedEventBus { get; set; }

    /// <summary>
    /// 创建文件夹事件
    /// </summary>
    /// <returns></returns>
    private async Task OnClickAsync()
    {
        await StorageApi.CreateDirectoryAsync(new CloudStoage.Domain.HttpModule.Input.CreateDirectoryInput
        {
            Path = Name,
            StorageId = StorageId,
        });
        await DialogChanged.InvokeAsync(false);
        await DistributedEventBus.PublishAsync(nameof(Storages), "创建文件夹成功");
    }
}
