using CoursesShop.Client.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CoursesShop.Client.Services
{
    public static class MaduleServicesDependacies
    {
        public static IServiceCollection AddServicesDependacies(this IServiceCollection services, IConfiguration configuration)
        {
            var backEndUrl = new Uri(configuration["Ip"]);
            services.AddRefitClient<IAuthenticationServices>()
                   .ConfigureHttpClient((sp, httpClient) =>
                   {
                       httpClient.BaseAddress = backEndUrl;

                   });

            services.AddRefitClient<IStudentServices>()
                .ConfigureHttpClient((sp, httpClient) =>
                {
                    httpClient.BaseAddress = backEndUrl;
                });

            services.AddRefitClient<ITeacherServices>()
                .ConfigureHttpClient((sp, httpClient) =>
                {
                    httpClient.BaseAddress = backEndUrl;
                });

            services.AddRefitClient<ITeacherServices>()
                .ConfigureHttpClient((sp, httpClient) =>
                {
                    httpClient.BaseAddress = backEndUrl;
                });

            services.AddRefitClient<ICourseServices>()
                .ConfigureHttpClient((sp, httpClient) =>
                {
                    httpClient.BaseAddress = backEndUrl;
                });

            services.AddRefitClient<IReviewServices>()
                .ConfigureHttpClient((sp, httpClient) =>
                {
                    httpClient.BaseAddress = backEndUrl;
                });

            services.AddRefitClient<IUserServices>()
                .ConfigureHttpClient((sp, httpClient) =>
                {
                    httpClient.BaseAddress = backEndUrl;
                });
            return services;
        }
    }
}
