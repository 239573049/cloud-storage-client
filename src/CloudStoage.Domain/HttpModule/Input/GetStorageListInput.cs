using CloudStoage.Domain.HttpModule.Result;

namespace CloudStoage.Domain.HttpModule.Input;

public class GetStorageListInput : PagedRequestDto
{
    /// <summary>
    /// 搜索
    /// </summary>
    public string? Keywords { get; set; }

    /// <summary>
    /// 上层文件夹id
    /// </summary>
    public Guid? StorageId { get; set; }
}
