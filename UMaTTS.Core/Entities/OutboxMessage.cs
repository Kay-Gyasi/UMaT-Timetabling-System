namespace UMaTLMS.Core.Entities;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime DateOccurred { get; set; }
    public DateTime? DateProcessed { get; set; }
    public string? Error { get; set; }
}
