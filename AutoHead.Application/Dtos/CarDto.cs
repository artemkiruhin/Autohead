namespace AutoHead.Application.Dtos;

public class CarDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime Released { get; set; }
    public decimal TimeTo100 { get; set; }
    public decimal TimeTo200 { get; set; }
    public decimal Price { get; set; }
    
    public Guid TypeId { get; set; }
    public string CarTypeName { get; set; }

    public Guid ManufacturerId { get; set; }
    public string ManufacturerName { get; set; }
    
    public Guid EngineId { get; set; }
    public string EngineName { get; set; } 
    public int HorsePower { get; set; }

    public Guid DriveId { get; set; }
    public string DriveName { get; set; }
    
    public Guid ColorId { get; set; }
    public string ColorName { get; set; }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}