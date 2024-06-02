using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Data.Results;
using Refit;

namespace CoursesShop.Client.Pages.Admin
{
    public partial class AdminTeacherPage
    {
        private IQueryable<Teacher>? Teachers;
        public AdminTeacherPage()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var token = await _localStore.GetItemAsync<string>("token");
                var result = await _teacherServices.GetAll(token);
                if (result.Succeeded)
                {
                    Teachers = result.Data!.AsQueryable();
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

        public async Task DeleteAsync(Teacher teacher)
        {
            var dialog = await DialogService.ShowConfirmationAsync($"Do you want to delete {teacher.FullName}?", "Yes", "No", "Delete Teacher");
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    var token = await _localStore.GetItemAsync<string>("token");
                    var response = await _teacherServices.Delete(token, teacher.Id);
                    if (response.Succeeded)
                    {
                        var dialogSuccee = await DialogService.ShowSuccessAsync("The action was a success");

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
            }
        }

        public async Task GetAsync(Teacher teacher)
        {
            _navigationManager.NavigateTo($"/Admin/Teachers/{teacher.Id}/Details");
        }
    }
}
