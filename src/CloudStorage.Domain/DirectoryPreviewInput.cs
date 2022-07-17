namespace CloudStorage.Domain;

public class DirectoryPreviewInput
{
    public DirectoryPreviewInput()
    {
    }

    public DirectoryPreviewInput(bool succeed, Guid? storageId)
    {
        Succeed = succeed;
        StorageId = storageId;
    }

    /// <summary>
    /// 是否上传
    /// </summary>
    public bool Succeed { get; set; } =false;

    /// <summary>
    /// 上级文件夹Id
    /// </summary>
    public Guid? StorageId { get; set; }


}
