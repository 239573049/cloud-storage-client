namespace CloudStorage.Domain
{
    public class PagedResultDto<T>
    {
        public IReadOnlyList<T> Items;

        /// <summary>
        /// 总数
        /// </summary>
        public long TotalCount { get; set; }

        public PagedResultDto()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="items"></param>
        public PagedResultDto(long totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}
