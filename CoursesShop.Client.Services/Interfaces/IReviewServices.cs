using CoursesShop.Client.Models.Bases;
using CoursesShop.Client.Models.Entities;
using Refit;

namespace CoursesShop.Client.Services.Interfaces
{
    public interface IReviewServices
    {
        [Get("/Reviews/{Id}")]
        public Task<Response<List<Review>>> GetByCourseId(string Id);
    }
}
