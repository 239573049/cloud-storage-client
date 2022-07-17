namespace CloudStorage.Domain;

public class GetStorageListInput
{
    /// <summary>
    /// 搜索
    /// </summary>
    public string? Keywords { get; set; }

    /// <summary>
    /// 上层文件夹id
    /// </summary>
    public Guid? StorageId { get; set; }

    /// <summary>
    /// 是否渲染
    /// </summary>
    public bool Refresh { get; set; } =true;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 50;
}
