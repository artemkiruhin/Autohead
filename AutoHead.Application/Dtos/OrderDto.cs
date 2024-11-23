namespace AutoHead.Application.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } 
    public string CustomerEmail { get; set; } 
    public string CustomerPhone { get; set; } 
    
    public Guid CarId { get; set; }
    public string CarName { get; set; }
    public string ManufacturerName { get; set; }
    public decimal Price { get; set; }
    public string ColorName { get; set; }
    
    
    public DateTime Created { get; set; }
    public string? Details { get; set; }
}