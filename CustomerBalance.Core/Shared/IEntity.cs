namespace CustomerBalance.Core.Shared;

public interface IEntity
{
    public Guid? Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public bool IsActive { get; set; }
}
