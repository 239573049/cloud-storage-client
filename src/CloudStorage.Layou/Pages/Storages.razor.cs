using CloudStoage.Domain.HttpModule.Input;
using CloudStoage.Domain.HttpModule.Result;

namespace CloudStorage.Layou.Pages;


partial class Storages
{
    private bool HasFybctuib;

    public Guid? StorageId { get; set; } 
    public GetStorageListInput GetStorageListInput { get; set; } = new GetStorageListInput();

    /// <summary>
    /// 列表
    /// </summary>
    public PagedResultDto<StorageDto> StorageList { get; set; } = new PagedResultDto<StorageDto>();

    [Inject]
    public StorageApi StorageApi { get; set; }
    private void OnFunctionClick(Guid id)
    {
        StorageId=id;
        HasFybctuib = true;
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetStorageListAsync();
    }

    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <returns></returns>
    private async Task GetStorageListAsync()
    {
        StorageList = await StorageApi.GetStorageListAsync(GetStorageListInput);
    }
}