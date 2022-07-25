using CloudStoage.Domain.HttpModule.Input;
using CloudStoage.Domain.HttpModule.Result;

namespace CloudStorage.Layou.Pages;


partial class Storages
{
    private bool HasFybctuib;
    private bool Load = false;
    /// <summary>
    /// 当前文件夹id
    /// </summary>
    public Guid StorageId { get; set; } = Guid.Empty;

    /// <summary>
    /// 当前点击的文件id
    /// </summary>
    public Guid? ClickStorageId { get; set; }
    public GetStorageListInput GetStorageListInput { get; set; } = new GetStorageListInput();

    /// <summary>
    /// 列表
    /// </summary>
    public PagedResultDto<StorageDto> StorageList { get; set; } = new PagedResultDto<StorageDto>();

    [Inject]
    public StorageApi StorageApi { get; set; }
    private void OnFunctionClick(Guid id)
    {
        ClickStorageId = id;
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
        Load=true;
        StorageList = await StorageApi.GetStorageListAsync(GetStorageListInput);
        Load=false;
    }

    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    private async Task RefreshAsync()
    {
        await GetStorageListAsync();
    }
}