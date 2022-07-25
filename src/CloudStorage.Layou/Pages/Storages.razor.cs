using CloudStoage.Domain.HttpModule.Input;
using CloudStoage.Domain.HttpModule.Result;

namespace CloudStorage.Layou.Pages;


partial class Storages
{
    private bool HasFybctuib;

    /// <summary>
    /// ��ǰ�ļ���id
    /// </summary>
    public Guid StorageId { get; set; } = Guid.Empty;

    /// <summary>
    /// ��ǰ������ļ�id
    /// </summary>
    public Guid? ClickStorageId { get; set; }
    public GetStorageListInput GetStorageListInput { get; set; } = new GetStorageListInput();

    /// <summary>
    /// �б�
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
    /// ��ȡ�����б�
    /// </summary>
    /// <returns></returns>
    private async Task GetStorageListAsync()
    {
        StorageList = await StorageApi.GetStorageListAsync(GetStorageListInput);
    }
}