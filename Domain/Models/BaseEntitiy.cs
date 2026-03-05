namespace CarShopFinal.Domain.Models;

public class BaseEntitiy
{
    public Guid Id { get; protected set; }
    
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    
    public void SetCreatedAt()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    
    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}