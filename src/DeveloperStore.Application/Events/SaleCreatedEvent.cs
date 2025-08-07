namespace DeveloperStore.Application.Events;

public class SaleCreatedEvent : BaseEvent
{
    public Guid SaleId { get; set; }
    public string Number { get; set; } = null!;
}

public class SaleCancelledEvent : BaseEvent
{
    public Guid SaleId { get; set; }
}
