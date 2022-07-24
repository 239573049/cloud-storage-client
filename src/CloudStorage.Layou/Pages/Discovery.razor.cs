
using CloudStoage.Domain.HttpModule.Result;

namespace CloudStorage.Layou.Pages;

partial class Discovery
{
    [Inject] public StorageApi StorageApi { get; set; }

    /// <summary>
    /// 最近文件信息
    /// </summary>
    public GetNewestStorageDto NewestStorage{ get; set; } = new GetNewestStorageDto();


    protected override async Task OnInitializedAsync()
    {
        await GetNewestStorageAsync();
    }

    private async Task GetNewestStorageAsync()
    {
        var result = await StorageApi.GetNewestFile();

        NewestStorage = result.Data;
    }
}
