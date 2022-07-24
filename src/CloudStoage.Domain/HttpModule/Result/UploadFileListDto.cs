namespace CloudStoage.Domain.HttpModule.Result;

public class UploadFileListDto
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 上传参数
    /// </summary>
    public string? Name { get; set; }

    public Stream? Stream { get; set; }
}
