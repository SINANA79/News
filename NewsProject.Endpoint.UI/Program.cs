using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NewsProject.Endpoint.UI;
using NewsProject.Endpoint.UI.Services;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
// TODO: this should be limited only to specified sources
builder.WithOrigins("https://localhost:5001")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .WithExposedHeaders("*"));
});



await builder.Build().RunAsync();
