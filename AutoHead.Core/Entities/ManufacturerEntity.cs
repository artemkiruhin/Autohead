namespace AutoHead.Core.Entities;

public class ManufacturerEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
    
    public static ManufacturerEntity Create(string name)
    {
        return new ManufacturerEntity()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}