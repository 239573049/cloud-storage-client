using Microsoft.AspNetCore.Components.Web;

namespace CloudStorage.Layou.Pages;

partial class PersonalCenter
{
    [Inject]
    public NavigationManager? Navigation { get; set; }

    private void OnLogoutClick(MouseEventArgs args)
    {
        Navigation.NavigateTo("/login");
    }
}
