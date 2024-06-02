using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Data.Results;
using Microsoft.AspNetCore.Components;
using Refit;

namespace CoursesShop.Client.Pages.Admin
{
    public partial class TeacherByIdPage
    {
        [Parameter]
        public string Id { get; set; }
        public Teacher Teacher { get; set; }
        public string BackendUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            BackendUrl = Configuration["IpForImage"];
            try
            {
                var token = await _localStore.GetItemAsync<string>("token");
                var result = await _teacherServices.GetById(token, Id);
                if (result.Succeeded)
                {
                    Teacher = result.Data;
                }
            }
            catch (ApiException ex)
            {
                var errors = await ex.GetContentAsAsync<Response<JwtAuthResult>>();
                await DialogService.ShowErrorAsync(errors.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await base.OnInitializedAsync();
        }
    }
}
