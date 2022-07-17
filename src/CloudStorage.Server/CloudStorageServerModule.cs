using CloudStorage.Pages;
using Token.MAUI.Module;
using Token.MAUI.Module.Attributes;

namespace CloudStorage.Server
{
    [DependsOn(
        typeof(CloudStoragePagesModule)
        )]
    public class CloudStorageServerModule : MauiModule
    {
    }
}
