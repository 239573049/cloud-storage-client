
using CloudStoage.Domain.HttpModule.Result;
using CloudStorage.Layou.Pages;
using Token.EventBus;

namespace CloudStorage.Layou.Components;

partial class Storagefile
{
    private bool hasFybctuib;

    [Parameter]
    public bool HasFybctuib
    {
        get { return hasFybctuib; }
        set
        {
            hasFybctuib = value;
            ValueChange.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<bool> ValueChange { get; set; }

    /// <summary>
    /// 文件or文件夹id
    /// </summary>
    [Parameter]
    public Guid? StorageId { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }

    [Inject]
    public IDistributedEventBus<bool> DistributedEventBus { get; set; }

    [Inject]
    public IDistributedEventBus<string> StringDstributedEventBus { get; set; }

    /// <summary>
    /// 信息
    /// </summary>
    public StorageDto Storageo { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.TryGetValue(nameof(StorageId), out Guid? storageId);
        if (storageId == null)
            return;

        parameters.TryGetValue(nameof(HasFybctuib), out bool hasFybctuib);

        if (hasFybctuib == HasFybctuib)
        {
            return;
        }
        HasFybctuib = hasFybctuib;

        parameters.TryGetValue(nameof(ValueChange), out EventCallback<bool> valueChange);

        ValueChange = valueChange;

        await GetStorageAsync(storageId);

        await HasFybctuibAsync();

        StateHasChanged();
    }

    private async Task HasFybctuibAsync()
    {
        await DistributedEventBus.Subscribe("HasFybctuib", async (data) =>
        {
            var result = data as bool?;
            if (result != null)
            {
                HasFybctuib = (bool)result;
                StateHasChanged();
                await StringDstributedEventBus.PublishAsync(nameof(Storages),"删除文件成功");
            }
        });
    }

    private async Task GetStorageAsync(Guid? id)
    {
        if (StorageId == id && Storageo != null)
        {
            return;
        }
        Storageo = await StorageApi.GetStorageAsync(id);

        StorageId = id;
    }
}