using MyWebApp.Models;
using MyWebApp.Configurations;
using MyWebApp.Repositories;
using MyWebApp.Services;
using MongoDB.Driver;
using MyWebApp.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Lägger till HttpContextAccessor som behövs för LocalImageService
builder.Services.AddHttpContextAccessor();

// Kopplar AzureBlob-konfigurationen från appsettings till vår klass
builder.Services.Configure<AzureBlobOptions>(
    builder.Configuration.GetSection(AzureBlobOptions.SectionName));

// Kolla om vi ska använda Azure Blob Storage via feature flag
bool useAzureStorage = builder.Configuration.GetValue<bool>("FeatureFlags:UseAzureStorage");

if (useAzureStorage)
{
    // Registrera Azure Blob Storage för produktion
    builder.Services.AddSingleton<IImageService, AzureBlobImageService>();
    Console.WriteLine("✅ Using Azure Blob Storage for images");
}
else
{
    // Registrera lokal bildhantering för utveckling
    builder.Services.AddSingleton<IImageService, LocalImageService>();
    Console.WriteLine("✅ Using local storage for images");
}

// Check if MongoDB should be used (default to false if not specified)
bool useMongoDb = builder.Configuration.GetValue<bool>("FeatureFlags:UseMongoDb");

if (useMongoDb)
{
    // Konfigurera MongoDB-alternativ
    builder.Services.Configure<MongoDbOptions>(
        builder.Configuration.GetSection(MongoDbOptions.SectionName));

    // Skapa MongoDB-klient
    builder.Services.AddSingleton<IMongoClient>(serviceProvider => {
        var mongoDbOptions = builder.Configuration.GetSection(MongoDbOptions.SectionName).Get<MongoDbOptions>();
        return new MongoClient(mongoDbOptions?.ConnectionString);
    });

    // Koppla in collectionen (där prenumeranter sparas)
    builder.Services.AddSingleton<IMongoCollection<Subscriber>>(serviceProvider => {
        var mongoDbOptions = builder.Configuration.GetSection(MongoDbOptions.SectionName).Get<MongoDbOptions>();
        var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
        var database = mongoClient.GetDatabase(mongoDbOptions?.DatabaseName);
        return database.GetCollection<Subscriber>(mongoDbOptions?.SubscribersCollectionName);
    });

    // Registrera MongoDB repository
    builder.Services.AddSingleton<ISubscriberRepository, MongoDbSubscriberRepository>();

    Console.WriteLine("✅ Using MongoDB repository");
}
else
{
    // Om vi INTE använder MongoDB → använd InMemoryRepository
    builder.Services.AddSingleton<ISubscriberRepository, InMemorySubscriberRepository>();

    Console.WriteLine("✅ Using in-memory repository");
}

// Registerar NewsletterService (som använder repositoryt ovan)
builder.Services.AddScoped<INewsletterService, NewsletterService>();


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
