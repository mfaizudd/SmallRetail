namespace SmallRetail.Web.Resources
{
    public class PagedResponse<T> : Response<T>
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public PagedResponse(T data) : base(data)
        { }
    }
}