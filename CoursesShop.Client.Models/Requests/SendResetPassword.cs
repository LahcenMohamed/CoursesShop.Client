using System.ComponentModel.DataAnnotations;

namespace CoursesShop.Client.Models.Requests
{
    public class SendResetPassword
    {
        [Required]
        public string UserName { get; set; }
    }
}
