using CloudStorage.Applications.Filter;
using Token.MAUI.Module;

namespace CloudStorage.Applications
{
    // All the code in this file is included in all platforms.
    public class CloudStorageApplicationsModule : MauiModule
    {

        public override void OnApplicationShutdown(MauiApp app)
        {
            var exceptionFilter = app.Services.GetRequiredService<ExceptionFilter>();

            AppDomain.CurrentDomain.FirstChanceException += exceptionFilter.ExceptionHandle;
        }
    }
}