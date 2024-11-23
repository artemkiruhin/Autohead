namespace AutoHead.Core.Entities;

public class CarEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime Released { get; set; }
    public decimal TimeTo100 { get; set; }
    public decimal TimeTo200 { get; set; }
    public decimal Price { get; set; }
    
    public Guid TypeId { get; set; }
    public virtual CarTypeEntity CarType { get; set; } = null!;

    public Guid ManufacturerId { get; set; }
    public virtual ManufacturerEntity Manufacturer { get; set; } = null!;
    
    public Guid EngineId { get; set; }
    public virtual EngineEntity Engine { get; set; } = null!;

    public Guid DriveId { get; set; }
    public virtual DriveEntity Drive { get; set; } = null!;
    
    public Guid ColorId { get; set; }
    public virtual ColorEntity Color { get; set; } = null!;
    
    public static CarEntity Create(string name, decimal to100, decimal to200, decimal price, Guid typeId, Guid manufacturerId, Guid engineId, Guid driveId, Guid colorId)
    {
        return new CarEntity()
        {
            Id = Guid.NewGuid(),
            Name = name,
            TimeTo100 = to100,
            TimeTo200 = to200,
            Price = price,
            TypeId = typeId,
            ManufacturerId = manufacturerId,
            EngineId = engineId,
            DriveId = driveId,
            ColorId = colorId
        };
    }
}