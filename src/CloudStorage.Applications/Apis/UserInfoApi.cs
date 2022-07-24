using CloudStoage.Domain;
using Microsoft.AspNetCore.Components.Forms;
using Token.MAUI.Module.Dependencys;

namespace CloudStorage.Applications.Apis;

public class UserInfoApi : IScopedDependency 
{
    private readonly IHttpClientFactory httpClientFactory;

    public UserInfoApi(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

}
