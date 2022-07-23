using Microsoft.JSInterop;

namespace CloudStorage.Layou;

public class InteractionJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public InteractionJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/CloudStorage.Layou/interaction.js").AsTask());
    }

    public async ValueTask ByIdClick(string id)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("ByIdClick", id);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}