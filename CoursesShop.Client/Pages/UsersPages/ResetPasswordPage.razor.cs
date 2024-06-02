using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Requests;
using CoursesShop.Data.Results;
using Microsoft.AspNetCore.Components;
using Refit;

namespace CoursesShop.Client.Pages.UsersPages
{
    public partial class ResetPasswordPage
    {
        [Parameter]
        public string UserName { get; set; }
        public ResetPasswordRequest ResetPasswordRequest { get; set; }

        protected override Task OnInitializedAsync()
        {
            ResetPasswordRequest = new ResetPasswordRequest()
            {
                UserName = UserName
            };
            return base.OnInitializedAsync();
        }

        public async Task Submit()
        {
            try
            {
                Console.WriteLine(ResetPasswordRequest.UserName);
                var result = await _athenticationServices.ResetPassword(ResetPasswordRequest);
                if (result.Succeeded)
                {
                    await DialogService.ShowConfirmationAsync("success");
                    _navigationManager.NavigateTo(@"/");
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
