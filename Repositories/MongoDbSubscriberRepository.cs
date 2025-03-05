using MyWebApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.Repositories
{
    public class MongoDbSubscriberRepository : ISubscriberRepository
    {
        // Vi sparar kopplingen till vår collection här
        private readonly IMongoCollection<Subscriber> _subscribers;

        // Här får vi in collectionen via Dependency Injection
        public MongoDbSubscriberRepository(IMongoCollection<Subscriber> subscribers)
        {
            _subscribers = subscribers;
        }

        // Hämta alla prenumeranter
        public async Task<IEnumerable<Subscriber>> GetAllAsync()
        {
            return await _subscribers.Find(_ => true).ToListAsync();
        }

        // Hämta prenumerant via e-post
        public async Task<Subscriber?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return await _subscribers.Find(s => s.Email == email).FirstOrDefaultAsync();
        }

        // Lägg till prenumerant
        public async Task<bool> AddAsync(Subscriber subscriber)
        {
            if (subscriber == null || string.IsNullOrEmpty(subscriber.Email))
                return false;

            var existingSubscriber = await GetByEmailAsync(subscriber.Email);
            if (existingSubscriber != null)
                return false;

            try
            {
                await _subscribers.InsertOneAsync(subscriber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Uppdatera prenumerant
        public async Task<bool> UpdateAsync(Subscriber subscriber)
        {
            if (subscriber == null || string.IsNullOrEmpty(subscriber.Email))
                return false;

            try
            {
                var result = await _subscribers.ReplaceOneAsync(
                    s => s.Email == subscriber.Email,
                    subscriber,
                    new ReplaceOptions { IsUpsert = false });

                return result.ModifiedCount > 0;
            }
            catch
            {
                return false;
            }
        }

        // Radera prenumerant via e-post
        public async Task<bool> DeleteAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                var result = await _subscribers.DeleteOneAsync(s => s.Email == email);
                return result.DeletedCount > 0;
            }
            catch
            {
                return false;
            }
        }

        // Kolla om prenumerant finns via e-post
        public async Task<bool> ExistsAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return await _subscribers.CountDocumentsAsync(s => s.Email == email) > 0;
        }
    }
}
