
namespace CloudStorage.Layou.Components;

partial class Storagefile
{
    [Parameter]
    public bool HasFybctuib { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChange{ get; set; }

    /// <summary>
    /// �ļ�or�ļ���id
    /// </summary>
    [Parameter]
    public Guid? StorageId { get; set; }
}