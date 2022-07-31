namespace CloudStorage.Domain.Shared;

public enum UpdateStats
{
    /// <summary>
    /// 正在上传
    /// </summary>
    BeUploading = 0,

    /// <summary>
    /// 成功
    /// </summary>
    Succeed,

    /// <summary>
    /// 失败
    /// </summary>
    Failure,

    /// <summary>
    /// 暂停
    /// </summary>
    Suspend
}
