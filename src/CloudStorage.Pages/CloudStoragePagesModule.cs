using CloudStorage.Applications;
using Microsoft.Extensions.DependencyInjection;
using Token.MAUI.Module;
using Token.MAUI.Module.Attributes;

namespace CloudStorage.Pages
{
	[DependOn(
		typeof(CloudStorageApplicationsModule)
		)]
	public class CloudStoragePagesModule : MauiModule
	{
		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddMasaBlazor();
		}
	}
}
