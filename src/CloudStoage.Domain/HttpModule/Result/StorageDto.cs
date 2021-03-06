using CloudStorage.Domain.Shared;

namespace CloudStoage.Domain.HttpModule.Result;


/// <summary>
/// 
/// </summary>
public class StorageDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public StorageType Type { get; set; }

    /// <summary>
    /// 云盘实际路径
    /// </summary>
    public string? StoragePath { get; set; }

    /// <summary>
    /// 虚拟路径
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 上级文件夹Id
    /// </summary>
    public Guid? StorageId { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long? Length { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public Guid UserInfoId { get; set; }

    /// <summary>
    /// 图标显示
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// url
    /// </summary>
    public string? CloudUrl { get; set; }

    public DateTime CreationTime { get; set; }
}