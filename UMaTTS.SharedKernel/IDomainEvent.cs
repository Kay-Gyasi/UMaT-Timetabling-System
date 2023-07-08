using MediatR;

namespace UMaTLMS.SharedKernel;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent(int? id)
    {
        Id = Guid.NewGuid();
        EntityId = id;
        OccurredOn = DateTime.UtcNow;
    }

    public Guid Id { get; }
    public int? EntityId { get; }
    public DateTime OccurredOn { get; }
}
