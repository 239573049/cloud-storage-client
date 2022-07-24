namespace CloudStoage.Domain.HttpModule.Result;

public class PagedResultDto<T>
{
    /// <inheritdoc />
    public long TotalCount { get; set; }

    private IReadOnlyList<T> _items;
    public IReadOnlyList<T> Items
    {
        get => _items ??= (new List<T>());
        set => _items = value;
    }

    public PagedResultDto()
    {
    }

    public PagedResultDto(long totalCount, IReadOnlyList<T> items)
    {
        TotalCount = totalCount;
        Items = items;
    }
}
