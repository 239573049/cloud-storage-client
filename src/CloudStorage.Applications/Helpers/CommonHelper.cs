using Token.Module.Dependencys;

namespace CloudStorage.Applications.Helpers;

public class CommonHelper : IScopedDependency
{
    public string GetFileSize(long? size)
    {
        if (size == null)
            return "";

        var num = 1024.00; //byte

        if (size < num)
            return size + "B";
        if (size < Math.Pow(num, 2))
            return ((long)size / num).ToString("f2") + "K"; //kb
        if (size < Math.Pow(num, 3))
            return ((long)size / Math.Pow(num, 2)).ToString("f2") + "M"; //M
        if (size < Math.Pow(num, 4))
            return ((long)size / Math.Pow(num, 3)).ToString("f2") + "G"; //G

        return ((long)size / Math.Pow(num, 4)).ToString("f2") + "T"; //T
    }
}
