using CloudStorage.Applications;
using Microsoft.Extensions.DependencyInjection;
using Token.EventBus;
using Token.MAUI.Module;
using Token.MAUI.Module.Attributes;

namespace CloudStorage.Layou;

[DependOn(
    typeof(CloudStorageApplicationsModule))]
public class CloudStorageLayouModule : MauiModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddEventBus();
        services.AddMasaBlazor();
    }
}
