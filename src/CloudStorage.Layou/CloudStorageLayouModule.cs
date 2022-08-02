using BlazorComponent;
using CloudStorage.Applications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Token.EventBus;
using Token.Module;
using Token.Module.Attributes;

namespace CloudStorage.Layou;

[DependOn(
    typeof(TokenEventBusModule),
    typeof(CloudStorageApplicationsModule))]
public class CloudStorageLayouModule : TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton(config);

        services.AddMasaBlazor();
    }
}
