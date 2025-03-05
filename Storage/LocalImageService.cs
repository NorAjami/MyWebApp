using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MyWebApp.Storage
{
    /// <summary>
    /// Hämtar bilder från den lokala wwwroot/images-mappen under utveckling.
    /// </summary>
    public class LocalImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // DI för att hämta information om servern och nuvarande request
        public LocalImageService(
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetImageUrl(string imageName)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";

            // Skapar hela URL:en till bilden i wwwroot/images
            return $"{baseUrl}/images/{imageName}";
        }
    }
}
