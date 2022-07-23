using Masa.Blazor;

namespace CloudStorage.Layou.Shared;

partial class Login
{
    private bool _valid = true;
    private MForm? _form;
    private bool PasswordShow;
    [Inject] public NavigationManager? navigation { get; set; }
    async Task ValidateAsync()
    {
        await _form.ValidateAsync()!;

    }
}
