using CloudStoage.Domain.HttpModule.Input;
using CloudStoage.Domain.HttpModule.Result;
using CloudStorage.Applications.Helpers;
using CloudStorage.Domain.Shared;
using Token.EventBus;

namespace CloudStorage.Layou.Pages;


partial class Storages
{
    private bool CreateFolder = false;
    private bool HasFybctuib;
    private bool Load = false;

    public bool DialogImagesShow { get; set; } = false;

    public string? DialogImagesSrc { get; set; }

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
    public IKeyLocalEventBus<string> DistributedEventBus { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }

    [Inject]
    public CommonHelper CommonHelper { get; set; }

    private async Task ClickInputFileAsync()
    {
        await CommonHelper.PickAndShow(GetStorageListInput.StorageId);
    }

    /// <summary>
    /// 功能按键点击事件
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    private async Task OnFunctionClickAsync(StorageDto dto)
    {
        ClickStorageId = dto.Id;
        if (dto.Type == StorageType.File)
        {
            HasFybctuib = true;
        }
        else
        {
            HasFybctuib = true;
        }
    }

    private async Task GetStorageAsync(StorageDto dto)
    {
        ClickStorageId = dto.Id;
        if (dto.Type == StorageType.File)
        {
            if (FileNameSuffix.Img.Any(x => dto.Path?.EndsWith(x) == true))
            {
                DialogImagesShow = true;
                DialogImagesSrc = dto.CloudUrl;
            }
        }
        else
        {
            GetStorageListInput.StorageId = dto.Id;
            await GetStorageListAsync();

        }
    }

    protected override async Task OnInitializedAsync()
    {
        await GetStorageListAsync();
        StorageListBus();
    }

    private void StorageListBus()
    {
        DistributedEventBus.Subscribe(nameof(Storages), async x =>
        {
            await GetStorageListAsync();
            StateHasChanged();
        });
    }

    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <returns></returns>
    private async Task GetStorageListAsync()
    {
        Load = true;
        StorageList = await StorageApi.GetStorageListAsync(GetStorageListInput);
        Load = false;
    }

    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    private async Task RefreshAsync()
    {
        await GetStorageListAsync();
    }

    /// <summary>
    /// 返回上一级
    /// </summary>
    /// <returns></returns>
    private async Task GOBackAsync()
    {
        var id = await StorageApi.GoBackAsync(GetStorageListInput.StorageId);
        GetStorageListInput.StorageId = id;
        await GetStorageListAsync();
    }

}