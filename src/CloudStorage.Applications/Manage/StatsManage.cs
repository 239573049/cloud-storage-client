using Token.Module.Dependencys;

namespace CloudStorage.Applications.Manage;

public class StatsManage : IScopedDependency
{
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; }
}
