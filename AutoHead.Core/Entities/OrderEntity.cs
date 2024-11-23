namespace AutoHead.Core.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    
    public Guid CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;
    
    public Guid CarId { get; set; }
    public virtual CarEntity Car { get; set; } = null!;
    
    public DateTime Created { get; set; }
    public string? Details { get; set; }
    
    public static OrderEntity Create(Guid customerId, Guid carId, string? details)
    {
        return new OrderEntity()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            CarId = carId,
            Details = details,
            Created = DateTime.UtcNow
        };
    }
}