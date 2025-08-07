namespace DeveloperStore.Application.Events;

public interface IEvent
{
    DateTime OccurredAt { get; }
    string EventType { get; }
}
