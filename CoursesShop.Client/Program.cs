using Blazored.LocalStorage;
using CoursesShop.Client;
using CoursesShop.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>()["Ip"]) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddServicesDependacies(builder.Configuration);

builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();
