using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Requests;
using Refit;

namespace CoursesShop.Client.Pages.Admin
{
    public partial class AdminUsersPage
    {
        private IQueryable<User> users;
        private int currentPage;
        private int TotalPages;
        private string token;

        protected override async Task OnInitializedAsync()
        {
            token = await _localStore.GetItemAsync<string>("token");
            currentPage = 1;
            var response = await UserServices.GetAllByPagintedAsync(token, new PaginatedResquest { PageNumber = 1, PageSize = 10 });
            users = response.Data.AsQueryable();
            TotalPages = response.TotalPages;
            Console.WriteLine(TotalPages);
            await base.OnInitializedAsync();
        }

        private async Task GoNextAsync()
        {
            if (currentPage < TotalPages)
            {
                var response = await UserServices.GetAllByPagintedAsync(token, new PaginatedResquest { PageNumber = currentPage + 1, PageSize = 10 });
                users = response.Data.AsQueryable();
                currentPage++;
            }
        }

        private async Task GoBackAsync()
        {
            if (currentPage > 1)
            {
                var response = await UserServices.GetAllByPagintedAsync(token, new PaginatedResquest { PageNumber = currentPage - 1, PageSize = 10 });
                users = response.Data.AsQueryable();
                currentPage--;
            }
        }

        private async Task DeleteAsync(User user)
        {
            var dialog = await DialogService.ShowConfirmationAsync($"Do you want to delete {user.UserName}?", "Yes", "No", "Delete Student");
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    //var response = await UserServices.DeleteAsync(token, user.Id);
                    //if (response.Succeeded)
                    //{
                    //    DialogService.ShowInfo($"Student {user.UserName} is deleted");
                    //    var getStudents = await UserServices.GetAllByPagintedAsync(token, new PaginatedResquest { PageNumber = currentPage, PageSize = 10 });
                    //    users = getStudents.Data.AsQueryable();
                    //}
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
}
