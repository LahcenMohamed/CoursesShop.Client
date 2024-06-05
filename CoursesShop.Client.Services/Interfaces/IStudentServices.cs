using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Requests;
using Refit;

namespace CoursesShop.Client.Services.Interfaces
{
    public interface IStudentServices
    {
        [Post("/Students")]
        public Task<Response<string>> AddAsync(StudentOrTeacher student);
        [Get("/Admin/Students/Paginated")]
        public Task<PaginatedResult<Student>> GetWithPaginatedAsync([Authorize] string token, [Query] PaginatedResquest resquest);
        [Delete("/Admin/Students/{Id}")]
        public Task<Response<string>> DeleteAsync([Authorize] string token, string Id);
    }
}
