
using CloudStoage.Domain.HttpModule.Result;
using Token.EventBus;

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

    [Inject]
    public IDistributedEventBus<bool> DistributedEventBus { get; set; }

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

    protected override async Task OnInitializedAsync()
    {
        await HasFybctuibAsync();
    }

    private async Task HasFybctuibAsync()
    {
        await DistributedEventBus.Subscribe(nameof(HasFybctuib), (data) =>
        {
            var result = data as bool?;
            if (result != null)
            {
                HasFybctuib = (bool)result;
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