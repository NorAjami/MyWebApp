using MyWebApp.Models;
using System.Collections.Concurrent;

namespace MyWebApp.Repositories;

// Här är vår trådsäkra implementation av ISubscriberRepository
public class InMemorySubscriberRepository : ISubscriberRepository
{
    // ConcurrentDictionary gör så vi kan hantera flera samtidiga användare utan problem
    private readonly ConcurrentDictionary<string, Subscriber> _subscribers = new(StringComparer.OrdinalIgnoreCase);

    // Hämta alla prenumeranter
    public Task<IEnumerable<Subscriber>> GetAllAsync()
    {
        return Task.FromResult(_subscribers.Values.AsEnumerable());
    }

    // Hämta en prenumerant via e-post
    public Task<Subscriber?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Task.FromResult<Subscriber?>(null);
        }

        _subscribers.TryGetValue(email, out var subscriber);
        return Task.FromResult(subscriber);
    }

    // Lägg till en prenumerant
    public Task<bool> AddAsync(Subscriber subscriber)
    {
        if (subscriber == null || string.IsNullOrEmpty(subscriber.Email))
        {
            return Task.FromResult(false);
        }

        // TryAdd returns true if the key was added, false if it already exists
        return Task.FromResult(_subscribers.TryAdd(subscriber.Email, subscriber));
    }

    // Uppdatera en prenumerant
    public Task<bool> UpdateAsync(Subscriber subscriber)
    {
        if (subscriber == null || string.IsNullOrEmpty(subscriber.Email))
        {
            return Task.FromResult(false);
        }

        // We need to handle the update manually since we need to check if key exists first
        if (!_subscribers.ContainsKey(subscriber.Email))
        {
            return Task.FromResult(false);
        }

        // Using AddOrUpdate to ensure thread safety
        _subscribers.AddOrUpdate(
            subscriber.Email,
            subscriber,
            (key, oldValue) => subscriber
        );

        return Task.FromResult(true);
    }

    // Radera en prenumerant via e-post
    public Task<bool> DeleteAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Task.FromResult(false);
        }

        // TryRemove returns true if the item was removed
        return Task.FromResult(_subscribers.TryRemove(email, out _));
    }

    // Kolla om prenumerant finns via e-post
    public Task<bool> ExistsAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(_subscribers.ContainsKey(email));
    }
}