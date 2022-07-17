using CloudStorage.Applications.Status;
using CloudStorage.Domain;
using Microsoft.AspNetCore.Components;

namespace CloudStorage.Applications.Services;

public class LoginService : IScopedDependency
{
    private readonly NavigationManager navigationManager;
    private readonly StatusManage statusManage;
    public LoginService(NavigationManager navigationManager, StatusManage statusManage)
    {
        this.navigationManager = navigationManager;
        this.statusManage = statusManage;
    }

    /// <summary>
    /// 登录校验
    /// </summary>
    /// <returns></returns>
    public async Task LoginVerifyAsync()
    {
        var token = await SecureStorage.Default.GetAsync(Constant.Token);

        if (string.IsNullOrEmpty(token))
        {
            return;
        }
        statusManage.Token = token;
        navigationManager.NavigateTo("/home",false);
    }

    /// <summary>
    /// 删除token并且返回登录界面
    /// </summary>
    /// <returns></returns>
    public async Task RemoveTokenAsync()
    {
        SecureStorage.Default.Remove(Constant.Token);
        statusManage.Token= null;
        navigationManager.NavigateTo("/",false);
    }
}
