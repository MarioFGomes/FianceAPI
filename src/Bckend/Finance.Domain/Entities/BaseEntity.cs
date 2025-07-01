

using Finance.Domain.Enum;

namespace Finance.Domain.Entities; 
public class BaseEntity {
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime UpdatedAt { get; set;}= DateTime.UtcNow;
    public BaseStatus Status { get; set; } = BaseStatus.created;

    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}
