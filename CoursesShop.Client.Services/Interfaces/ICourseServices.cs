using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Requests;
using CoursesShop.Client.Models.Responses;
using Refit;

namespace CoursesShop.Client.Services.Interfaces
{
    public interface ICourseServices
    {
        [Get("/Courses/Paginated")]
        public Task<PaginatedResult<CourseResponse>> GetByPaginated([Query] PaginatedResquest resquest);

        [Get("/Courses/{Id}")]
        public Task<Response<CourseResponse>> GetById(string Id);

        [Post("/Teacher/Courses")]
        public Task<Response<string>> AddAsync([Authorize] string token, Course course);
        [Put("/Teacher/Courses")]
        public Task<Response<string>> UpdateAsync([Authorize] string token, Course course);

        [Multipart]
        [Put("/Teacher/Courses/{Id}/photo")]
        public Task<Response<string>> UpdatePhotoAsync([Authorize] string token, string Id, [AliasAs("photo")] StreamPart Photo);
        [Get("/Teacher/Courses")]
        public Task<Response<List<CourseResponse>>> GetByTeacher([Authorize] string token);

        [Delete("/Admin/Courses/{Id}")]
        public Task<string> DeleteAsync([Authorize] string token, string Id);
        [Delete("/Teacher/Courses/{courseId}")]
        public Task<string> DeleteByTeacherAsync([Authorize] string token, string courseId);

    }
}
