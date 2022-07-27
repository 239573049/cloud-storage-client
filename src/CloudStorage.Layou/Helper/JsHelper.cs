using Microsoft.JSInterop;
using Token.MAUI.Module.Dependencys;

namespace CloudStorage.Layou.Helper;

public class JsHelper : IScopedDependency
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public JsHelper(IJSRuntime jsRuntime)
    {
        this.moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/CloudStorage.Layou/interaction.js").AsTask());
    }

    /// <summary>
    /// 点击指定id元素
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task ClickInputFileAsync(string id)
    {
        var js = await moduleTask.Value;
        await js.InvokeVoidAsync("ByIdClick", id);
    }

}
