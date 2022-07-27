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
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJ1c2VyIjoie1wiQWNjb3VudFwiOlwiYWRtaW5cIixcIlBhc3N3b3JkXCI6XCJhZG1pblwiLFwiTmFtZVwiOlwiYWRtaW5cIixcIkJyaWVmSW50cm9kdWN0aW9uXCI6bnVsbCxcIldlQ2hhdE9wZW5JZFwiOm51bGwsXCJIZWFkUG9ydHJhaXRzXCI6bnVsbCxcIlNleFwiOjAsXCJTdGF0dXNcIjowLFwiQ2xvdWRTdG9yYWdlUm9vdFwiOlwiLi93d3dyb290L0Nsb3VkU3RvcmFnZVxcXFxhYzRkZWRmYTFlYmU0YzNhOWJmY2JmZGQ2NWQxZjNkMlwiLFwiSXNEZWxldGVkXCI6ZmFsc2UsXCJDcmVhdGlvblRpbWVcIjpcIjAwMDEtMDEtMDFUMDA6MDA6MDBcIixcIkV4dHJhUHJvcGVydGllc1wiOnt9LFwiQ29uY3VycmVuY3lTdGFtcFwiOlwiZjQwMGJiZThjZDI0NDlkNWEyNTFjZmMxM2FmMzEyOGNcIixcIklkXCI6XCI0OTU4MWRkNC0yYzM5LTQ2NzAtOTE5Yi01MjMyY2UwZTllM2VcIn0iLCJpZCI6IjQ5NTgxZGQ0LTJjMzktNDY3MC05MTliLTUyMzJjZTBlOWUzZSIsImV4cCI6MTc2MjEzNjE0OCwiaXNzIjoidG9rZW5odS50b3AiLCJhdWQiOiJ0b2tlbmh1LnRvcCJ9.D-EpFpkPvg8o4W2OF0ZbiKCv2mqqErfCmJ0bN8KJYD8");
                x.BaseAddress = new Uri(Constant.Api);
               
            })
            .ConfigureHttpMessageHandlerBuilder(builder =>
            {
                builder.PrimaryHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (m, c, ch, e) => true,
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