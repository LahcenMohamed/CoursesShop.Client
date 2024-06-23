using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using CoursesShop.Client.Models.Requests;
using Refit;

namespace CoursesShop.Client.Services.Interfaces
{
    public interface IUserServices
    {
        [Get("/Users")]
        public Task<Response<List<User>>> GetAllAsync();
        [Get("/Users/Paginated")]
        public Task<PaginatedResult<User>> GetAllByPagintedAsync([Authorize] string token, [Query] PaginatedResquest resquest);
    }
}
