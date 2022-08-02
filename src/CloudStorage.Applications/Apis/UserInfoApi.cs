using CloudStoage.Domain.HttpModule;
using CloudStoage.Domain.HttpModule.Result;
using Newtonsoft.Json;
using Token.Module.Dependencys;

namespace CloudStorage.Applications.Apis;

public class UserInfoApi : IScopedDependency
{
    private const string Name = "api/userinfo";

    private readonly IHttpClientFactory httpClientFactory;

    public UserInfoApi(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// 获取用户基本信息
    /// </summary>
    /// <returns></returns>
    public async Task<UserInfoDto> GetAsync()
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var data = await httpclient.GetStringAsync(Name);

        var result = JsonConvert.DeserializeObject<ModelStateResult<UserInfoDto>>(data);

        return result.Data;
    }
}
