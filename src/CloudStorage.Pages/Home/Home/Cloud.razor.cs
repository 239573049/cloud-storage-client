using CloudStorage.Domain;

namespace CloudStorage.Pages.Home.Home;

partial class Cloud
{
    [Parameter]
    public Action<StorageDto>? StorageClickAction { get; set; }

    [Parameter]
    public GetStorageListInput Input { get; set; } = new();

    [Parameter]
    public Action<GetStorageListInput>? GetStorageListAction { get; set; }
}
