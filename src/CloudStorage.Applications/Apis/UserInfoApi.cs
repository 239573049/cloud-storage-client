using Token.Module.Dependencys;

namespace CloudStorage.Applications.Apis;

public class UserInfoApi : IScopedDependency 
{
    private readonly IHttpClientFactory httpClientFactory;

    public UserInfoApi(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

}
