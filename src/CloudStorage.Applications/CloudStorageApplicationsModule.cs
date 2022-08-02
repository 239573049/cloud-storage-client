using CloudStoage.Domain;
using CloudStorage.Applications.Manage;
using CloudStorage.Domain.Shared;
using Microsoft.Extensions.Configuration;
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
                var configuration = services.GetRequiredService<IConfiguration>();


                x.DefaultRequestHeaders.Add(Constant.Authorization, $"Bearer " + token.Token);

                // 如果是nginx代理了路由最后要加/不然无法找到路径
                x.BaseAddress = new Uri(configuration["HostApi"].EndsWith("/") ? configuration["HostApi"] : configuration["HostApi"] + "/");

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