
using CloudStoage.Domain.HttpModule.Result;
using CloudStorage.Layou.Pages;
using Token.EventBus;

namespace CloudStorage.Layou.Components;

partial class Storagefile
{
    [Parameter]
    public bool HasFybctuib { get; set; }

    [Parameter]
    public EventCallback<bool> HasFybctuibChanged { get; set; }

    /// <summary>
    /// 文件or文件夹id
    /// </summary>
    [Parameter]
    public StorageDto Storage { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }

    [Inject]
    public IKeyLocalEventBus<bool> DistributedEventBus { get; set; }

    [Inject]
    public IKeyLocalEventBus<string> StringDstributedEventBus { get; set; }


    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.TryGetValue("Storage", out StorageDto storage);
        if (storage == null)
            return;

        Storage = storage;

        parameters.TryGetValue(nameof(HasFybctuib), out bool hasFybctuib);

        if (hasFybctuib == HasFybctuib)
        {
            return;
        }
        HasFybctuib = hasFybctuib;

        parameters.TryGetValue(nameof(HasFybctuibChanged), out EventCallback<bool> valueChange);

        HasFybctuibChanged = valueChange;


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
                await HasFybctuibChanged.InvokeAsync(result ?? false);
                StateHasChanged();
                await StringDstributedEventBus.PublishAsync(nameof(Storages), "删除文件成功");
            }
        });
    }

}