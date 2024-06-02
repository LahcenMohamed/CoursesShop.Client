using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using Refit;

namespace CoursesShop.Client.Services.Interfaces
{
    public interface IStudentServices
    {
        [Post("/Students")]
        public Task<Response<string>> AddAsync(StudentOrTeacher student);
    }
}
