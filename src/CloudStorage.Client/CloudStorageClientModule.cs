using CloudStorage.Pages;
using Token.MAUI.Module;
using Token.MAUI.Module.Attributes;

namespace CloudStorage.Client
{
    [DependOn(
        typeof(CloudStoragePagesModule)
        )]
    public class CloudStorageClientModule : MauiModule
    {

    }
}
