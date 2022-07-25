
using CloudStoage.Domain.HttpModule.Result;

namespace CloudStorage.Layou.Components;

partial class Storagefile
{
    [Parameter]
    public bool HasFybctuib { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChange { get; set; }

    /// <summary>
    /// 文件or文件夹id
    /// </summary>
    [Parameter]
    public Guid? StorageId { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }

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

        HasFybctuib = hasFybctuib;

        parameters.TryGetValue(nameof(ValueChange), out EventCallback<bool> valueChange);

        ValueChange = valueChange;

        await GetStorageAsync(storageId);

        StateHasChanged();
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