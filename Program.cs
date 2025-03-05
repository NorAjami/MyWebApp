using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWebApp.Services;
using MyWebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//  Registrera Repository som singleton (dela samma data i hela appen)
builder.Services.AddSingleton<ISubscriberRepository, InMemorySubscriberRepository>();

//  Registrera NewsletterService som scoped (ny instans per request)
builder.Services.AddScoped<INewsletterService, NewsletterService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
