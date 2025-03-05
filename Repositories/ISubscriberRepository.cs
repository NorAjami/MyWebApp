using MyWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.Repositories
{
    // Detta interface är kontraktet som bestämmer vad vår datahantering ska kunna göra.
    public interface ISubscriberRepository
    {
        // Hämta alla prenumeranter
        Task<IEnumerable<Subscriber>> GetAllAsync();

        // Hämta en prenumerant via e-post
        Task<Subscriber?> GetByEmailAsync(string email);

        // Lägg till en ny prenumerant
        Task<bool> AddAsync(Subscriber subscriber);

        // Uppdatera en prenumerant
        Task<bool> UpdateAsync(Subscriber subscriber);

        // Radera en prenumerant via e-post
        Task<bool> DeleteAsync(string email);

        // Kolla om en prenumerant finns via e-post
        Task<bool> ExistsAsync(string email);
    }
}
