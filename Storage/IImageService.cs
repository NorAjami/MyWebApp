namespace MyWebApp.Storage
{
    /// <summary>
    /// Interface för att hämta bild-URL baserat på bildens namn.
    /// Detta gör att vi kan byta mellan olika lösningar (t.ex. lokal lagring och Azure).
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Hämtar URL till en bild utifrån bildens namn, till exempel "hero.jpg".
        /// </summary>
        /// <param name="imageName">Namnet på bilden.</param>
        /// <returns>Fullständig URL till bilden.</returns>
        string GetImageUrl(string imageName);
    }
}
