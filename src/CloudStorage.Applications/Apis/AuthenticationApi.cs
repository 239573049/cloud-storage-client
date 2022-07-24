using CloudStoage.Domain;
using CloudStoage.Domain.HttpModule;
using CloudStoage.Domain.HttpModule.Input;
using Masa.Blazor;
using System.Net.Http.Json;
using Token.MAUI.Module.Dependencys;

namespace CloudStorage.Applications.Apis;

public class AuthenticationApi : IScopedDependency
{
    private const string Name = "api/authentication";
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IPopupService PopupService;
    public AuthenticationApi(IHttpClientFactory httpClientFactory, IPopupService popupService)
    {
        this.httpClientFactory = httpClientFactory;
        PopupService = popupService;
    }

    /// <summary>
    /// 获取token
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<string> LoginAsync(CreateTokenInput input)
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var message =await httpclient.PostAsJsonAsync(Name + "/login", input);

        if (message.IsSuccessStatusCode)
        {
            var data =await message.Content.ReadFromJsonAsync<ModelStateResult<string>>();

            if (data.Code == Constant.SuccessStatusCode)
            {
                return data.Data;
            }
            await PopupService.ToastErrorAsync(data.Message);
            return null;
        }
        throw new Exception();
    }
}
