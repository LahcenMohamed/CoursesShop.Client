using CoursesShop.Client.Models.Entities;

namespace CoursesShop.Client.Models.Requests
{
    public sealed class SignupRequest
    {
        public StudentOrTeacher StudentOrTeacher { get; set; }
        public User User { get; set; }
    }
}
