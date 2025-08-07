namespace DeveloperStore.Application.Events;

public abstract class BaseEvent : IEvent
{
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
    public string EventType => GetType().Name;
}
