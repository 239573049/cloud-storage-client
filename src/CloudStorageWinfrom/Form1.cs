using BlazorComponent;
using CloudStorage.Layou;
using CloudStorage.Layou.Shared;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.Metrics;
using Token.MAUI.Module.Extensions;

namespace CloudStorageWinfrom;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Blazor();
    }

    private void Blazor()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddModuleApplication<CloudStorageLayouModule>();
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