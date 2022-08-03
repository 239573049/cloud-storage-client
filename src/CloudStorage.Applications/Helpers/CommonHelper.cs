using CloudStoage.Domain.Etos;
using Token.EventBus.EventBus;
using Token.Module.Dependencys;

namespace CloudStorage.Applications.Helpers;

public class CommonHelper : IScopedDependency
{

    private readonly ILocalEventBus LocalEventBus;

    public CommonHelper(ILocalEventBus localEventBus)
    {
        LocalEventBus = localEventBus;
    }

    /// <summary>
    /// b转换格式
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public string GetFileSize(long? size)
    {
        if (size == null)
            return "";

        var num = 1024.00; //byte

        if (size < num)
            return size + "B";
        if (size < Math.Pow(num, 2))
            return ((long)size / num).ToString("f2") + "KB"; //kb
        if (size < Math.Pow(num, 3))
            return ((long)size / Math.Pow(num, 2)).ToString("f2") + "MB"; //M
        if (size < Math.Pow(num, 4))
            return ((long)size / Math.Pow(num, 3)).ToString("f2") + "GB"; //G

        return ((long)size / Math.Pow(num, 4)).ToString("f2") + "TB"; //T
    }

    public async Task PickAndShow(Guid? storageId)
    {
        PickOptions options = new()
        {
            PickerTitle = "请选择需要上传的文件",
        };

        try
        {
            var result = await FilePicker.Default.PickMultipleAsync(options);
            if (result != null)
            {

                var uploadings = result.Select(x => new UploadingEto
                {
                    Id = Guid.NewGuid(),
                    FileName = x.FileName,
                    FilePath = x.FullPath,
                    StorageId = storageId,
                }).ToList();

                _ = LocalEventBus.PublishAsync(uploadings, false);
            }

        }
        catch (Exception ex)
        {
        }

    }
}
