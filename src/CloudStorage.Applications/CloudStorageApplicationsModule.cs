using CloudStoage.Domain;
using CloudStorage.Applications.Manage;
using Token.EventBus;
using Token.Module;
using Token.Module.Attributes;

namespace CloudStorage.Applications;

[DependOn(
    typeof(TokenEventBusModule))]
public class CloudStorageApplicationsModule : TokenModule
{
    public override async void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient(string.Empty)
            .ConfigureHttpClient((services, x) =>
            {
                var status = services.GetService<StatsManage>();
                x.DefaultRequestHeaders.Add(Constant.Authorization, $"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJ1c2VyIjoie1wiQWNjb3VudFwiOlwiYWRtaW5cIixcIlBhc3N3b3JkXCI6XCJhZG1pblwiLFwiTmFtZVwiOlwiYWRtaW5cIixcIkJyaWVmSW50cm9kdWN0aW9uXCI6bnVsbCxcIldlQ2hhdE9wZW5JZFwiOm51bGwsXCJIZWFkUG9ydHJhaXRzXCI6XCJodHRwczovL3RzMS5jbi5tbS5iaW5nLm5ldC90aD9pZD1PSVAtQy5JYmdZY2JDSGZVVXRmd2VHTUtBalR3QUFBQSZ3PTI1MCZoPTI1MCZjPTgmcnM9MSZxbHQ9OTAmbz02JnBpZD0zLjEmcm09MlwiLFwiU2V4XCI6MCxcIlN0YXR1c1wiOjAsXCJDbG91ZFN0b3JhZ2VSb290XCI6XCIuL3d3d3Jvb3QvQ2xvdWRTdG9yYWdlL2FjNGRlZGZhMWViZTRjM2E5YmZjYmZkZDY1ZDFmM2QyXCIsXCJJc0RlbGV0ZWRcIjpmYWxzZSxcIkNyZWF0aW9uVGltZVwiOlwiMDAwMS0wMS0wMVQwMDowMDowMFwiLFwiRXh0cmFQcm9wZXJ0aWVzXCI6e30sXCJDb25jdXJyZW5jeVN0YW1wXCI6XCJmNDAwYmJlOGNkMjQ0OWQ1YTI1MWNmYzEzYWYzMTI4Y1wiLFwiSWRcIjpcIjQ5NTgxZGQ0LTJjMzktNDY3MC05MTliLTUyMzJjZTBlOWUzZVwifSIsImlkIjoiNDk1ODFkZDQtMmMzOS00NjcwLTkxOWItNTIzMmNlMGU5ZTNlIiwiZXhwIjoxNzYyMjI2NDM0LCJpc3MiOiJ0b2tlbmh1LnRvcCIsImF1ZCI6InRva2VuaHUudG9wIn0.Qga-H-eKWrguw6ojk3ps1lW0sdx2XVxfmlZPvtjAWSg");
                x.BaseAddress = new Uri(Constant.Api);
               
            })
            .ConfigureHttpMessageHandlerBuilder(builder =>
            {
                builder.PrimaryHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
                  
                }; 
            });

       await services.Initialize();
    }

}