using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Responses;
using Microsoft.FluentUI.AspNetCore.Components;
using Refit;

namespace CoursesShop.Client.Pages.Teachers
{
    public partial class CoursesTeacherPage
    {
        private List<CourseResponse> courses;
        private string backendUrl;

        public CoursesTeacherPage()
        {
            courses = new List<CourseResponse>();
        }

        private async Task Add()
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<AddCourseDialog>(new Course(), new DialogParameters()
                {
                    Height = "500px",
                    Width = "380px",
                    Title = "Add new Course",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                });

                var result = await dialog.Result;
                if (!result.Cancelled && result.Data != null)
                {
                    var token = await LocalStore.GetItemAsync<string>("token");
                    var response = await CoursesServices.AddAsync(token, (Course)result.Data);
                }
            }
            catch (ApiException ex)
            {
                DialogService.ShowInfo(ex.Message);
            }
            catch (Exception ex)
            {
                DialogService.ShowInfo("errors");
            }


        }

        private async Task Update(CourseResponse courseResponse)
        {
            try
            {
                var course = new Course { Id = courseResponse.Id, Title = courseResponse.Title, Description = courseResponse.Description, Price = courseResponse.Price };
                var dialog = await DialogService.ShowDialogAsync<AddCourseDialog>(course, new DialogParameters()
                {
                    Height = "500px",
                    Width = "380px",
                    Title = "Add new Course",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                });

                var result = await dialog.Result;
                if (!result.Cancelled && result.Data != null)
                {
                    var token = await LocalStore.GetItemAsync<string>("token");
                    var response = await CoursesServices.UpdateAsync(token, (Course)result.Data);
                    await DialogService.ShowSuccessAsync("update course sccess");
                }
            }
            catch (ApiException ex)
            {
                var errors = await ex.GetContentAsAsync<Response<string>>();
                DialogService.ShowError(errors.Message);
            }
            catch (Exception ex)
            {
                DialogService.ShowError(ex.Message);
            }


        }

        private async Task UpdatePhoto(string courseId)
        {
            try
            {
                var dialog = await DialogService.ShowDialogAsync<UpdateCoursePhotoDialog>(default!, new DialogParameters()
                {
                    Height = "400px",
                    Width = "500px",
                    Title = "Add photo Course",
                    PreventDismissOnOverlayClick = true,
                    PreventScroll = true,
                });

                var result = await dialog.Result;
                if (!result.Cancelled && result.Data != null)
                {
                    var token = await LocalStore.GetItemAsync<string>("token");
                    var stream = (Stream)result.Data;
                    var streamPart = new StreamPart(stream, "course.jpg", "image/jpg");
                    var response = await CoursesServices.UpdatePhotoAsync(token, courseId, streamPart);
                    await DialogService.ShowInfoAsync("success");
                }
            }
            catch (ApiException ex)
            {
                var errors = await ex.GetContentAsAsync<Response<string>>();
                await DialogService.ShowErrorAsync("Errror: " + errors.Message);
            }
            catch (Exception ex)
            {
                await DialogService.ShowErrorAsync(ex.Message);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            backendUrl = Configuration["IpForImage"];
            try
            {
                var token = await LocalStore.GetItemAsync<string>("token");
                var response = await CoursesServices.GetByTeacher(token);
                courses = response.Data;
            }
            catch (ApiException ex)
            {
                var errors = await ex.GetContentAsAsync<Response<string>>();
                await DialogService.ShowErrorAsync(errors.Data);
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
                    var response = await CoursesServices.DeleteByTeacherAsync(token, Id);
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
            NavigationManager.NavigateTo($"/Teacher/Courses/{id}");
        }
    }
}
