namespace MyWebApp.Configurations
{
    /// <summary>
    /// Håller inställningar för Azure Blob Storage, hämtade från appsettings.json.
    /// </summary>
    public class AzureBlobOptions
    {
        public const string SectionName = "AzureBlob";

        // URL till Azure Blob-containern där bilderna ligger.
        public string ContainerUrl { get; set; } = string.Empty;
    }
}
