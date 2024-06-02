using Blazored.LocalStorage;
using CoursesShop.Client;
using CoursesShop.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>()["Ip"]) });

builder.Services.AddBlazoredLocalStorage();


builder.Services.AddRefitClient<IAuthenticationServices>()
    .ConfigureHttpClient((sp, httpClient) =>
    {
        httpClient.BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>()["Ip"]);

    });
builder.Services.AddRefitClient<IStudentServices>()
    .ConfigureHttpClient((sp, httpClient) =>
    {
        httpClient.BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>()["Ip"]);
    });
builder.Services.AddRefitClient<ITeacherServices>()
    .ConfigureHttpClient((sp, httpClient) =>
    {
        httpClient.BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>()["Ip"]);
    });
builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();
