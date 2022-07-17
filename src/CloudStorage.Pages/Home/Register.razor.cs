using CloudStorage.Domain;
using Masa.Blazor;

namespace CloudStorage.Pages.Home;

partial class Register
{
    private bool _valid = true;
    private MForm? _form;
    private LogionInput _model = new();
    private bool PasswordShow;

    [Inject] public NavigationManager? navigation { get; set; }

    async Task ValidateAsync()
    {
        await _form?.ValidateAsync();
        navigation!.NavigateTo("/home", false);
    }
}
