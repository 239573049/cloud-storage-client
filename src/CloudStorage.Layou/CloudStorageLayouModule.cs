using Microsoft.Extensions.DependencyInjection;
using Token.MAUI.Module;

namespace CloudStorage.Layou;

//[DependOn(
//    typeof(CloudStorageApplicationsModule))]
public class CloudStorageLayouModule : MauiModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddMasaBlazor();
    }
}
