namespace WatchBuddy.Services.Order.Domain.Core;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedOn { get; set; }
    public DateTime DeletedOn { get; set; }
}