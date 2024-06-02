using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using Refit;

namespace CoursesShop.Client.Services.Interfaces
{
    public interface ITeacherServices
    {
        [Get("/Admin/Teachers")]
        public Task<Response<List<Teacher>>> GetAll([Authorize] string token);
        [Get("/Admin/Teachers/{Id}")]
        public Task<Response<Teacher>> GetById([Authorize] string token, string Id);

        [Delete("/Admin/Teachers/{Id}")]
        public Task<Response<string>> Delete([Authorize] string token, string Id);
    }
}
