namespace CloudStorage.Layou.Components.Dialogs;

partial class DialogImages
{
    [Parameter]
    public bool Show { get; set; }

    [Parameter]
    public EventCallback<bool> ShowChanged { get; set; }


    [Parameter]
    public string? Src
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<string?> SrcChanged { get; set; }


}
