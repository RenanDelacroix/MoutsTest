using MongoDB.Driver;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Application.Events;

public class MongoEventPublisher : IEventPublisher
{
    private readonly IMongoCollection<IEvent> _collection;
    private readonly ILogger<MongoEventPublisher> _logger;

    public MongoEventPublisher(IMongoClient mongoClient, ILogger<MongoEventPublisher> logger)
    {
        var database = mongoClient.GetDatabase("DeveloperStore");
        _collection = database.GetCollection<IEvent>("EventLog");
        _logger = logger;
    }

    public async Task PublishAsync(IEvent @event)
    {
        _logger.LogInformation("Event published: {EventType} at {Time}", @event.EventType, @event.OccurredAt);
        await _collection.InsertOneAsync(@event);
    }
}
