using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Requests;
using CoursesShop.Client.Models.Responses;
using Refit;

namespace CoursesShop.Client.Pages.Admin
{
    public partial class CoursesAdminPage
    {
        private List<CourseResponse> courses;
        private string backendUrl;

        public CoursesAdminPage()
        {
            courses = new List<CourseResponse>();
        }

        protected override async Task OnInitializedAsync()
        {
            backendUrl = Configuration["IpForImage"];
            try
            {
                PaginatedResquest resquest = new PaginatedResquest { PageNumber = 1, PageSize = 10 };
                var response = await CoursesServices.GetByPaginated(resquest);
                courses = response.Data;
            }
            catch (ApiException ex)
            {
                var errors = await ex.GetContentAsAsync<Response<string>>();
                await DialogService.ShowErrorAsync(errors.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await base.OnInitializedAsync();
        }

        private async Task DeleteAsync(string Id, string title)
        {
            var dialog = await DialogService.ShowConfirmationAsync($"Do you want to delete {title}?", "Yes", "No", "Delete Teacher");
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    var token = await LocalStore.GetItemAsync<string>("token");
                    var response = await CoursesServices.DeleteAsync(token, Id);
                    var dialogSuccee = await DialogService.ShowSuccessAsync("The action was a success");
                }
                catch (ApiException ex)
                {
                    var errors = await ex.GetContentAsAsync<Response<string>>();
                    await DialogService.ShowErrorAsync(errors.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task GetById(string id)
        {
            NavigationManager.NavigateTo($"/Admin/Courses/{id}");
        }
    }
}
