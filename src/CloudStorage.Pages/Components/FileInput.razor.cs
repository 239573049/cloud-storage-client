using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace CloudStorage.Pages.Components;

partial class FileInput
{
    public int FileSize { get; set; } = 10;

    [Inject] private IJSRuntime Js { get; set; }

    [Parameter]
    public Action<IReadOnlyList<IBrowserFile>> BrowserFiles { get; set; }
    protected async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        BrowserFiles.Invoke(e.GetMultipleFiles(FileSize));

        await Task.CompletedTask;
    }
}
