using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Responses;
using Microsoft.AspNetCore.Components;
using Refit;

namespace CoursesShop.Client.Pages.Teachers
{
    public partial class CourseDetailsPage
    {
        [Parameter]
        public string Id { get; set; }
        private CourseResponse course;
        private List<Review> reviews;
        private string backendUrl;

        public CourseDetailsPage()
        {
            reviews = new List<Review>();
        }
        protected override async Task OnInitializedAsync()
        {
            backendUrl = Configuration["IpForImage"];
            try
            {
                var response = await CoursesServices.GetById(Id);
                course = response.Data;
                var reviewResponse = await ReviewServices.GetByCourseId(Id);
                reviews = reviewResponse.Data;
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
    }
}
