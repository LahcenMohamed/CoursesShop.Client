﻿using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Requests;
using CoursesShop.Data.Results;
using Refit;

namespace CoursesShop.Client.Pages.UsersPages
{
    public partial class Login
    {

        public LoginModel LoginModel { get; set; }
        public Login()
        {
            LoginModel = new LoginModel();
        }

        public async void Submit()
        {
            try
            {
                var result = await _athenticationServices.SignIn(LoginModel);
                if (result.Succeeded)
                {
                    await _localStore.SetItemAsync("token", result.Data.AccessToken);
                    if (result.Data.refreshToken.UserType == "Admin")
                    {
                        _navigationManager.NavigateTo("Admin");
                    }
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
