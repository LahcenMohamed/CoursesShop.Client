namespace CoursesShop.Client.Models.Requests
{
    public sealed class PaginatedResquest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? Search { get; set; }
    }
}
