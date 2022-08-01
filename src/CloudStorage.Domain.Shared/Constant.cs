namespace CloudStoage.Domain;

public class Constant
{
    public const string Token = "token";

#if DEBUG
    public const string Api = "https://localhost:8081";
#else
    public const string Api = "https://tokenhu.top";
#endif

    public const string DateTimeStr = "yyyy-MM-dd HH-mm-ss";

    public const string Authorization = "Authorization";

    /// <summary>
    /// 响应状态码
    /// </summary>
    public const string SuccessStatusCode = "200";
}
