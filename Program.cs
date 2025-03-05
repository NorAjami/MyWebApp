using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWebApp;
using MyWebApp.Services;
using MyWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register FluentValidation
//builder.Services.AddScoped<IValidator<MyHelloWorldMVC.Models.Subscriber>, UserValidator>();
//builder.Services.AddFluentValidationAutoValidation();

// Register INewsletterService
builder.Services.AddScoped<INewsletterService, NewsletterService>(); // Ensure NewsletterService is implemented

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
