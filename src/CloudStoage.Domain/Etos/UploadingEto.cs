namespace CloudStoage.Domain.Etos;

public class UploadingEto
{
    public Guid Id { get; set; }

    /// <summary>
    /// /文件名称
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 文件夹Id
    /// </summary>
    public Guid? StorageId { get; set; }

    /// <summary>
    /// 长度
    /// </summary>
    public long? Length { get; set; }

    /// <summary>
    /// Stream
    /// </summary>
    public string FilePath { get; set; }
}
