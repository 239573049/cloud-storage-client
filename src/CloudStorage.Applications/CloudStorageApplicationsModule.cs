using CloudStorage.Applications.Filter;
using Token.MAUI.Module;
using CloudStorage.Applications.Helpers;
using CloudStorage.Domain;
using CloudStorage.Applications.Status;

namespace CloudStorage.Applications
{

    public class CloudStorageApplicationsModule : MauiModule
    {
        public override async void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient(string.Empty)
                .ConfigureHttpClient((services, x) =>
            {
                var status = services.GetService<StatusManage>();
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer {status.Token}");
                x.BaseAddress = new Uri(Constant.Api);
            }).ConfigureHttpMessageHandlerBuilder(builder =>
            {
                builder.PrimaryHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
                };
            });
        }

        public override void OnApplicationShutdown(MauiApp app)
        {
            var exceptionFilter = app.Services.GetRequiredService<CloudStorageExceptionFilter>();

            AppDomain.CurrentDomain.FirstChanceException += exceptionFilter.ChanceException;
        }
    }
}