namespace UMaTLMS.SharedKernel;

public abstract class Entity
{
    public int Id { get; protected set; }
    public Audit? Audit { get; set; }

    public Entity AuditAs(Audit audit)
    {
        Audit = audit;
        return this;
    }

    private List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }
}

public class Audit
{
    private Audit()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        CreatedBy = "admin";
        UpdatedBy = "admin";
    }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public string CreatedBy { get; private set; }
    public string UpdatedBy { get; private set; }
    public EntityStatus Status { get; private set; } = EntityStatus.Normal;

    public static Audit Create() => new();

    public Audit WasCreatedBy(string? name)
    {
        if (string.IsNullOrEmpty(name)) return this;
        CreatedBy = name;
        return this;
    }

    public Audit Update(string name)
    {
        UpdatedBy = name;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public Audit Delete()
    {
        Status = EntityStatus.Deleted;
        return this;
    }

    public Audit Archive()
    {
        Status = EntityStatus.Archived;
        return this;
    }
}

public enum EntityStatus
{
    Normal = 1,
    Archived,
    Deleted
}
