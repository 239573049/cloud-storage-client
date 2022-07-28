using BlazorComponent;
using CloudStorage.Layou;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Token.Module.Extensions;

namespace CloudStorageWinfrom;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Blazor();
    }

    private async void Blazor()
    {
        IServiceCollection services = new ServiceCollection();
        await services.AddModuleApplication<CloudStorageLayouModule>();
        services.AddWindowsFormsBlazorWebView();
        blazorWebView1.HostPage = "wwwroot\\index.html";
        blazorWebView1.Services = services.BuildServiceProvider();
        services.AddWindowsFormsBlazorWebView();
#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
        blazorWebView1.RootComponents.Add<Main>("#app");
    }
}