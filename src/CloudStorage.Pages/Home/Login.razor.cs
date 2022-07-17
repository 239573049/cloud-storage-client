using CloudStorage.Applications.Apis;
using CloudStorage.Applications.Services;
using CloudStorage.Domain;
using Masa.Blazor;
using OneOf.Types;

namespace CloudStorage.Pages.Home;

partial class Login
{
    private bool _valid = true;
    private MForm? _form;
    private LogionInput _model = new();
    private bool PasswordShow;
    [Inject] public NavigationManager? navigation { get; set; }
    [Inject] public AuthenticationApi? authenticationApi { get; set; }
    [Inject] public LoginService? loginService { get; set; }
    async Task ValidateAsync()
    {
        await _form.ValidateAsync()!;

        var token = await authenticationApi!.LoginAsync(_model);
        navigation!.NavigateTo("/home", false);
    }

    protected override async Task OnInitializedAsync()
    {
        await loginService!.LoginVerifyAsync();
    }
}
