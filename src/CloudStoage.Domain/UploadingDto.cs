using CloudStorage.Domain.Shared;

namespace CloudStoage.Domain;

public class UploadingDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// /文件名称
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 长度
    /// </summary>
    public long Length { get; set; } = 0;

    /// <summary>
    /// 已经上传的大小
    /// </summary>
    public long UploadingSize { get; set; } = 0;

    /// <summary>
    /// 上传速率（上一秒上传大小）
    /// </summary>
    public int Rate { get; set; }

    /// <summary>
    /// 上传状态
    /// </summary>
    public UpdateStats Stats { get; set; }

    /// <summary>
    /// 上传进度
    /// </summary>
    public double Progress
    {
        get
        {
            if (UploadingSize == 0 || Length == 0)
            {
                return 0;
            }

            return Math.Round((double)(UploadingSize / (decimal)Length) * 100,2);
        }
    }
}
