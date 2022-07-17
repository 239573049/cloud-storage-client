
using CloudStorage.Domain;
using CloudStorage.Domain.Shared;
using CloudStorage.Pages.Home.Home;

namespace CloudStorage.Pages.Home;

partial class HomeLayou
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    public bool FloatingShow { get; set; }
    public string Component { get; set; } = nameof(Discovery);

    /// <summary>
    /// 当前云盘目录
    /// </summary>
    private StorageDto _storageDto=new();

    [Parameter]
    public GetStorageListInput Input { get; set; } = new();

    /// <summary>
    /// 是否菜单
    /// </summary>
    public bool Menu { get; set; } = false;

    private void OnDiscoveryStorage(StorageDto dto)
    {
        _storageDto = dto;
        StateHasChanged();
    }

    private void OnReturnPrevious()
    {
        if (_storageDto.Type == StorageType.Directory)
        {
            Input.StorageId = _storageDto.StorageId;
            Input.Refresh = true;
        }
    }

    private void OnGetStorageList(GetStorageListInput input)
    {
        if (input.StorageId !=null)
        {
            Menu = true;
        }
        else
        {
            Menu = false;
        }

        if (input.StorageId != Input.StorageId)
        {
            Input = input;
        }
    }

    private void OnFloatingWindowClick()
    {
        FloatingShow = true;
    }

    private void OnFloatingShow(bool value)
    {
        FloatingShow = value;
        if (!value)
        {
            Input.Refresh = true;
        }
    }

    private void SetComponent(string name)
    {
        if (Component != name)
        {
            Component = name;
            StateHasChanged();
        }
    }
}
