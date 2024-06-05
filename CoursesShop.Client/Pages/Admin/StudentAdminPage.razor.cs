using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Requests;
using Microsoft.FluentUI.AspNetCore.Components;
using Refit;

namespace CoursesShop.Client.Pages.Admin
{
    public partial class StudentAdminPage
    {
        private IQueryable<Student> students;
        private int currentPage;
        private int TotalPages;
        private string token;
        PaginationState pagination = new PaginationState { ItemsPerPage = 3 };

        protected override async Task OnInitializedAsync()
        {
            token = await _localStore.GetItemAsync<string>("token");
            currentPage = 1;
            var response = await StudentServices.GetWithPaginatedAsync(token, new PaginatedResquest { PageNumber = 1, PageSize = 10 });
            students = response.Data.AsQueryable();
            TotalPages = response.TotalPages;
            Console.WriteLine(TotalPages);
            await base.OnInitializedAsync();
        }

        private async Task GoNextAsync()
        {
            if (currentPage < TotalPages)
            {
                var response = await StudentServices.GetWithPaginatedAsync(token, new PaginatedResquest { PageNumber = currentPage + 1, PageSize = 10 }); students = response.Data.AsQueryable();
                students = response.Data.AsQueryable();
                currentPage++;
            }
        }

        private async Task GoBackAsync()
        {
            if (currentPage > 1)
            {
                var response = await StudentServices.GetWithPaginatedAsync(token, new PaginatedResquest { PageNumber = currentPage - 1, PageSize = 10 });
                students = response.Data.AsQueryable();
                currentPage--;
            }
        }

        private async Task DeleteAsync(Student student)
        {
            var dialog = await DialogService.ShowConfirmationAsync($"Do you want to delete {student.FullName}?", "Yes", "No", "Delete Student");
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                try
                {
                    var response = await StudentServices.DeleteAsync(token, student.Id);
                    if (response.Succeeded)
                    {
                        DialogService.ShowInfo($"Student {student.FullName} is deleted");
                        var getStudents = await StudentServices.GetWithPaginatedAsync(token, new PaginatedResquest { PageNumber = currentPage, PageSize = 10 });
                        students = getStudents.Data.AsQueryable();
                    }
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
