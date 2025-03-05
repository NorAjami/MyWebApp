using Microsoft.Extensions.Options;
using MyWebApp.Configurations;

namespace MyWebApp.Storage
{
    /// <summary>
    /// Hämtar bilder från Azure Blob Storage i produktion.
    /// </summary>
    public class AzureBlobImageService : IImageService
    {
        private readonly string _blobContainerUrl;

        public AzureBlobImageService(IOptions<AzureBlobOptions> options)
        {
            _blobContainerUrl = options.Value.ContainerUrl;
        }

        public string GetImageUrl(string imageName)
        {
            // Bygger URL till bilden på Azure Blob Storage
            return $"{_blobContainerUrl}/{imageName}";
        }
    }
}
