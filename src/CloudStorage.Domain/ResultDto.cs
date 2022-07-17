namespace CloudStorage.Domain
{
    public class ResultDto<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string? Message { get; set; }
    }

    public class ResultDto
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string? Message { get; set; }
    }
}
