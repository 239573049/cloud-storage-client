using CloudStorage.Applications.Extension;
using CloudStorage.Applications.Services;
using CloudStorage.Applications.Status;
using CloudStorage.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CloudStorage.Applications.Apis;


public class AuthenticationApi : IScopedDependency
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AuthenticationApi> _logger;
    private readonly StatusManage _staus;
    private readonly LoginService loginService;
    private const string Name = "api/authentication";
    public AuthenticationApi(IHttpClientFactory httpClientFactory, ILogger<AuthenticationApi> logger, StatusManage staus, LoginService loginService)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _staus = staus;
        this.loginService = loginService;
    }

    public async Task<string> LoginAsync(LogionInput input)
    {
        var httpClinet = _httpClientFactory.CreateClient();
        var reposrt = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
        var message = await httpClinet.PostAsync(Name + "/login", reposrt);
        var result = await message.Content.JsonFormAsync<ResultDto<string>>();
        if (result.Code == "200")
        {
            await SecureStorage.Default.SetAsync(Constant.Token, result.Data);
            _staus.Token = result.Data;
            return result.Data;
        }
        _logger.LogError("[error] {0} 登录异常：{1}", DateTime.Now, result.Message);
        throw new Exception(result.Message);
    }
}
