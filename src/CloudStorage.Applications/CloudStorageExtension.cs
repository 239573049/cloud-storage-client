using CloudStoage.Domain;
using CloudStorage.Applications.Filter;
using CloudStorage.Applications.Helpers;
using CloudStorage.Applications.Manage;

namespace CloudStorage.Applications;

public static class CloudStorageExtension
{
    public static async Task Initialize(this IServiceCollection services)
    {
        var statsManage = services.BuildServiceProvider().GetRequiredService<StatsManage>();
        if(statsManage != null)
        {
            statsManage.Token =await ConfigFile.GetConfigAsync(Constant.Token);
        }
    }

    public static void UseExceptionFilter(this MauiApp app)
    {

        var exceptionFilter = app.Services.GetRequiredService<ExceptionFilter>();

        AppDomain.CurrentDomain.FirstChanceException += exceptionFilter.ExceptionHandle;
    }
}
