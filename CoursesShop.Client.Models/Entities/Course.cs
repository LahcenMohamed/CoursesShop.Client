namespace CoursesShop.Client.Models.Entities
{
    public sealed class Course
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
