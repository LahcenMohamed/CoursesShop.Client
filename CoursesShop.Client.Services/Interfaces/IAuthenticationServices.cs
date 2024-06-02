using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Requests;
using CoursesShop.Data.Results;
using Refit;

namespace CoursesShop.Client.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        [Post("/Authentication/Signin")]
        public Task<Response<JwtAuthResult>> SignIn(LoginModel loginModel);

        [Post("/Authentication/SendResetPasswordCode")]
        public Task<Response<string>> SendResetPassword(SendResetPassword sendResetPassword);

        [Post("/Authentication/ResetPassword")]
        public Task<Response<string>> ResetPassword(ResetPasswordRequest resetPasswordRequest);

        [Post("/Authentication/Signup")]
        public Task<Response<string>> Signup(User user);

    }
}
