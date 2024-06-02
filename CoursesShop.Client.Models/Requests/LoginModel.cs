using System.ComponentModel.DataAnnotations;

namespace CoursesShop.Client.Models.Requests
{
    public class LoginModel
    {
        [Required]
        [MinLength(4)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
