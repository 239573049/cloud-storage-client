using CloudStorage.Domain.Shared;

namespace CloudStoage.Domain.HttpModule.Result;

public class UserInfoDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    public string? Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? BriefIntroduction { get; set; }

    /// <summary>
    /// 微信openid
    /// </summary>
    public string? WeChatOpenId { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? HeadPortraits { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public SexType Sex { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// 服务器文件
    /// </summary>
    public string? CloudStorageRoot { get; set; }

    public DateTime CreationTime { get; }

    /// <summary>
    /// 用户总大小
    /// </summary>
    public long TotalSize { get; set; }

    /// <summary>
    /// 已经使用大小
    /// </summary>
    public long UsedSize { get; set; } = 0;

    /// <summary>
    /// 使用占比
    /// </summary>
    public int Percentage
    {
        get
        {
            if (UsedSize == 0 || TotalSize == 0)
            {
                return 0;
            }
            var percentage = (UsedSize / (decimal)TotalSize) *100;
            return (int)percentage;
        }
    }
}
