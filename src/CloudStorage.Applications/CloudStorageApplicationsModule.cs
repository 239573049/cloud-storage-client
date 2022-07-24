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
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJ1c2VyIjoie1wiQWNjb3VudFwiOlwiYWRtaW5cIixcIlBhc3N3b3JkXCI6XCJhZG1pblwiLFwiTmFtZVwiOlwiYWRtaW5cIixcIkJyaWVmSW50cm9kdWN0aW9uXCI6bnVsbCxcIldlQ2hhdE9wZW5JZFwiOm51bGwsXCJIZWFkUG9ydHJhaXRzXCI6bnVsbCxcIlNleFwiOjAsXCJTdGF0dXNcIjowLFwiQ2xvdWRTdG9yYWdlUm9vdFwiOlwiLi93d3dyb290L0Nsb3VkU3RvcmFnZVxcXFw4MGQ3NDBiMTM3YTU0ZGYzOTU0ODAwNjg2ODNhMWZmYVwiLFwiSXNEZWxldGVkXCI6ZmFsc2UsXCJDcmVhdGlvblRpbWVcIjpcIjAwMDEtMDEtMDFUMDA6MDA6MDBcIixcIkV4dHJhUHJvcGVydGllc1wiOnt9LFwiQ29uY3VycmVuY3lTdGFtcFwiOlwiYThlOWJiZTc1MzM5NDk5NmJlZGNlNDY2MGZmMjQ2MzFcIixcIklkXCI6XCI1ODk5NjgxMC1mOWU5LTQzNGUtODNkZS1mYTQ3YTU0ODY0MGVcIn0iLCJpZCI6IjU4OTk2ODEwLWY5ZTktNDM0ZS04M2RlLWZhNDdhNTQ4NjQwZSIsImV4cCI6MTY1ODY4MDE1MSwiaXNzIjoidG9rZW5odS50b3AiLCJhdWQiOiJ0b2tlbmh1LnRvcCJ9.1MXnxAM8RwZlrhydlSQZHbzbZmE2326xbIYarfB_-Hk");
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