using CloudStoage.Domain;
using CloudStorage.Applications.Manage;
using CloudStorage.Domain.Shared;
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
                var token = services.GetRequiredService<TokenManage>();

                x.DefaultRequestHeaders.Add(Constant.Authorization, $"Bearer " + token.Token);

                // 如果是nginx代理了路由最后要加/不然无法找到路径
                x.BaseAddress = new Uri(Constant.Api.EndsWith("/") ? Constant.Api : Constant.Api + "/");

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