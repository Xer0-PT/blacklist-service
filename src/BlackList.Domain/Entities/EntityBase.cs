namespace BlackList.Domain.Entities;

public abstract class EntityBase
{
    public long Id { get; }
    public Guid FaceitId { get; set; }
    public string Nickname { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
