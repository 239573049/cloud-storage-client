using System.Runtime.ExceptionServices;
using Token.MAUI.Module.Dependencys;

namespace CloudStorage.Applications.Filter;

public class ExceptionFilter : IScopedDependency
{
    public void ExceptionHandle(object sender, FirstChanceExceptionEventArgs args)
    {

    }
}
