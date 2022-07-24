namespace CloudStoage.Domain.HttpModule.Result;

public class PagedRequestDto
{
    private int? _page = 1;
    private int? _pageSize = 20;
    /// <summary>
    /// 页码, 默认1
    /// </summary>
    public int? Page
    {
        get => _page;
        set => _page = value is null or <= 0 ? 1 : value;
    }

    /// <summary>
    /// 页大小, 默认20
    /// </summary>
    public int? PageSize
    {
        get => _pageSize;
        set => _pageSize = value is null or <= 0 ? 20 : value;
    }
}
