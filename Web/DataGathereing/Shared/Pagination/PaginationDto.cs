namespace DataGathering.Api.Shared.Pagination
{
    public sealed class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public IList<T> Items { get; set; }
    }
}
