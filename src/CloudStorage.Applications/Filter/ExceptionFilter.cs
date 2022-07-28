using CloudStoage.Domain;
using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;
using Token.Module.Dependencys;

namespace CloudStorage.Applications.Filter;

public class ExceptionFilter : IScopedDependency
{
    private readonly ILogger<ExceptionFilter> logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this.logger = logger;
    }

    public void ExceptionHandle(object sender, FirstChanceExceptionEventArgs args)
    {
        logger.LogError("时间:{0};异常：{1}",DateTime.Now.ToString(Constant.DateTimeStr), args.Exception);
    }
}
