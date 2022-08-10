using CloudStoage.Domain.HttpModule.Result;
using CloudStorage.Applications.Helpers;

namespace CloudStorage.Layou.Pages;

partial class PersonalCenter
{
    private UserInfoDto? UserInfo { get; set; } =new UserInfoDto();
    [Inject]
    public NavigationManager? Navigation { get; set; }

    [Inject]
    public CommonHelper CommonHelper { get; set; }

    [Inject]
    public UserInfoApi UserInfoApi { get; set; }

    private void OnLogoutClick(MouseEventArgs args)
    {
        Navigation?.NavigateTo("/login");
    }

    protected override async Task OnInitializedAsync()
    {
        await GetUserInfoAsync();
    }

    private async Task GetUserInfoAsync()
    {
         UserInfo = await UserInfoApi.GetAsync();
        var p= UserInfo.Percentage;
    }
}
