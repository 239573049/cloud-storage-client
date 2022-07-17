using CloudStorage.Applications.Apis;
using CloudStorage.Domain;

namespace CloudStorage.Pages.Components.Home;

partial class DiscoveryMessage
{
    private List<GetNewestStorageDto> newestStorages = new();

    [Inject] public StorageApi? StorageApi { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await StorageApi!.GetNewestFileAsync();
        if (result == null)
            return;
        newestStorages.Add(result);
        StateHasChanged();
    }

}
