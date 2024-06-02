using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Requests;
using CoursesShop.Data.Results;
using Refit;

namespace CoursesShop.Client.Pages.UsersPages
{
    public partial class ForgetPasswordPage
    {
        public SendResetPassword SendResetPassword { get; set; }

        public ForgetPasswordPage()
        {
            SendResetPassword = new SendResetPassword();
        }
        public async Task Submit()
        {
            try
            {
                var result = await _athenticationServices.SendResetPassword(SendResetPassword);
                if (result.Succeeded)
                {
                    _navigationManager.NavigateTo(@$"/Authentication/ResetPassword/{SendResetPassword.UserName}");
                }
            }
            catch (ApiException ex)
            {
                var errors = await ex.GetContentAsAsync<Response<JwtAuthResult>>();
                await DialogService.ShowErrorAsync(errors.Message);
            }
            catch (Exception ex)
            {
                await DialogService.ShowErrorAsync(ex.Message);
            }
        }
    }
}
