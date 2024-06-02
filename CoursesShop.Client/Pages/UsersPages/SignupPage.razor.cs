using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Requests;
using Microsoft.AspNetCore.Components;
using Refit;

namespace CoursesShop.Client.Pages.UsersPages
{
    public partial class SignupPage
    {
        private string selectedUserType;
        public SignupRequest Request { get; set; }
        public SignupPage()
        {
            Request = new SignupRequest() { User = new User(), StudentOrTeacher = new StudentOrTeacher() };
        }

        private void OnOptionSelected(ChangeEventArgs e)
        {
            selectedUserType = e.Value.ToString();
        }

        public async Task Submit()
        {
            try
            {
                string Id = string.Empty;
                Request.User.Type = selectedUserType;
                if (Request.User.Type == "Student")
                {
                    var response = await _studentServices.AddAsync(Request.StudentOrTeacher);
                    Id = response.Data;

                }
                else
                {
                    Console.WriteLine("not student");
                }
                Request.User.TypeId = Id;
                Request.User.Email = Request.StudentOrTeacher.Email;
                await _athenticationServices.Signup(Request.User);
                _navigationManager.NavigateTo("/Authentication/Login");

            }
            catch (ApiException ex)
            {
                var errors = await ex.GetContentAsAsync<Response<string>>();
                await DialogService.ShowErrorAsync(errors.Message);
            }
            catch (Exception ex)
            {
                await DialogService.ShowErrorAsync(ex.Message);
            }


        }
    }
}
