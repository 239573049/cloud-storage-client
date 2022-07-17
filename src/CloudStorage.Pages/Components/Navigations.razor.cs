using BlazorComponent;
using CloudStorage.Domain;
using CloudStorage.Pages.Home.Home;

namespace CloudStorage.Pages.Components;

partial class Navigations
{
    StringNumber value = 0;

    [Parameter]
    public Action<string?>? OnClick { get; set; }

    string Color
    {
        get
        {
            if (value == 0) return "teal";
            if (value == 1) return "indigo";
            if (value == 2) return "brown";
            return "blue-grey";
        }
    }

    private List<NavigationsTab> navigationsTabs = new();

    public Navigations()
    {
        navigationsTabs.Add(new NavigationsTab
        {
            Title = "发现",
            Name= nameof(Discovery)
        });

        navigationsTabs.Add(new NavigationsTab
        {
            Title = "云盘",
            Name = nameof(Cloud)
        });

        navigationsTabs.Add(new NavigationsTab
        {
            Title = "视频",
            Name = nameof(Video)
        });
        
        navigationsTabs.Add(new NavigationsTab
        {
            Title = "相册",
            Name = nameof(Album)
        });

        navigationsTabs.Add(new NavigationsTab
        {
            Title = "我的",
            Name = nameof(MyInfo)
        });
    }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        return base.SetParametersAsync(parameters);
    }

}