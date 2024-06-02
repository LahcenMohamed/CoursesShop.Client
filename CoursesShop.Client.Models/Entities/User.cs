namespace CoursesShop.Client.Models.Entities
{
    public sealed class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string TypeId { get; set; }
    }
}
