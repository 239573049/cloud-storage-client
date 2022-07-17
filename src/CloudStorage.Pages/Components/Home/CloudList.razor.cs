using CloudStorage.Applications.Apis;
using CloudStorage.Domain;

namespace CloudStorage.Pages.Components.Home;

partial class CloudList
{
    private PagedResultDto<StorageDto> storageList = new();

    [Parameter]
    public PagedResultDto<StorageDto> StorageList
    {
        get { return storageList; }
        set
        {

            storageList = value;
        }
    }

    [Inject] public StorageApi? StorageApi { get; set; }

    [Parameter]
    public Action<StorageDto>? StorageClickAction { get; set; }

    private GetStorageListInput input = new();

    [Parameter]
    public GetStorageListInput Input
    {
        get { return input; }
        set
        {
            if (value.Refresh)
            {
                GetStorageList();
            }
            GetStorageListAction?.Invoke(value);
            input = value;
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (input.Refresh)
        {
            Input.Refresh = false;
        }
    }

    [Parameter]
    public Action<GetStorageListInput>? GetStorageListAction { get; set; }


    protected override void OnInitialized()
    {
        GetStorageList();
    }

    private void SetStorageId(StorageDto dto)
    {
        if (dto.Type == Domain.Shared.StorageType.Directory)
        {
            Input.StorageId = dto.Id;
            GetStorageList();
            StorageClickAction?.Invoke(dto);
        }
    }

    private async void GetStorageList()
    {
        StorageList = await StorageApi!.GetStorageListAsync(Input);
        StateHasChanged();
    }
}
