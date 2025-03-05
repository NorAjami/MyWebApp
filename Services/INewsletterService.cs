using MyWebApp.Models;
using MyWebApp.Services;
using System.Collections.Generic;
using MyWebApp;


namespace MyWebApp.Services
{
    // Interface = kontraktet som bestämmer vad newsletter-servicen ska kunna göra.
    public interface INewsletterService
    {
        // Lägga till en ny prenumerant
        Task<OperationResult> SignUpForNewsletterAsync(Subscriber subscriber);

        // Avregistrera en prenumerant
        Task<OperationResult> OptOutFromNewsletterAsync(string email);

        // Hämta alla aktiva prenumeranter
        Task<IEnumerable<Subscriber>> GetActiveSubscribersAsync();
    }
}
