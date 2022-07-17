using CloudStorage.Applications.Status;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using System.Runtime.ExceptionServices;

namespace CloudStorage.Applications.Filter;

public class CloudStorageExceptionFilter : IScopedDependency
{
    private readonly IPopupService PopupService;
    private readonly StatusManage statusManage;
    private readonly NavigationManager navigationManager;
    public CloudStorageExceptionFilter(IPopupService popupService, StatusManage statusManage, NavigationManager navigationManager)
    {
        PopupService = popupService;
        this.statusManage = statusManage;
        this.navigationManager = navigationManager;
    }

    public async void ChanceException(object sender, FirstChanceExceptionEventArgs e)
    {
        if (e.Exception.Message == "账号未授权")
        {
            await PopupService.ToastAsync(e.Exception.Message, BlazorComponent.AlertTypes.Error);
            statusManage.Token = null;
            SecureStorage.Default.RemoveAll();
            navigationManager.NavigateTo("/", false);

        }
    }
}
