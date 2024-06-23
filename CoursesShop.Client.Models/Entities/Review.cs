namespace CoursesShop.Client.Models.Entities
{
    public sealed class Review
    {
        public string Id { get; set; }
        public double Evalution { get; set; }
        public string? Comment { get; set; }
        public string StudentName { get; set; }

    }
}
