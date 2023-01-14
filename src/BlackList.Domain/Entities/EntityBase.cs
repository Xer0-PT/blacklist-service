namespace BlackList.Domain.Entities;

public abstract class EntityBase
{
    public long Id { get; }
    public DateTimeOffset CreatedAt { get; set; }
}
