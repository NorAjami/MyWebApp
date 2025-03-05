namespace MyWebApp.Configurations
{
    // Den här klassen används för att hämta inställningar från appsettings.json
    public class MongoDbOptions
    {
        // Namnet på sektionen i appsettings.json
        public const string SectionName = "MongoDb";

        // Anslutningssträng till MongoDB
        public string ConnectionString { get; set; } = string.Empty;

        // Namnet på databasen vi ska använda
        public string DatabaseName { get; set; } = string.Empty;

        // Namnet på collection/tabellen som lagrar subscribers
        public string SubscribersCollectionName { get; set; } = string.Empty;
    }
}
