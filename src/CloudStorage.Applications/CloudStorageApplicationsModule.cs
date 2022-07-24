using CloudStoage.Domain;
using CloudStorage.Applications.Filter;
using CloudStorage.Applications.Manage;
using Token.MAUI.Module;

namespace CloudStorage.Applications;

public class CloudStorageApplicationsModule : MauiModule
{
    public override async void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient(string.Empty)
            .ConfigureHttpClient((services, x) =>
            {
                var status = services.GetService<StatsManage>();
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer {status.Token}");
                x.BaseAddress = new Uri(Constant.Api);
            }).ConfigureHttpMessageHandlerBuilder(builder =>
            {
                builder.PrimaryHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
                };
            });

       await services.Initialize();
    }

    public override void OnApplicationShutdown(MauiApp app)
    {
        var exceptionFilter = app.Services.GetRequiredService<ExceptionFilter>();

        AppDomain.CurrentDomain.FirstChanceException += exceptionFilter.ExceptionHandle;
    }
}