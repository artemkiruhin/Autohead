namespace AutoHead.Core.Entities;

public class EngineEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int HorsePower { get; set; }
    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
    
    public static EngineEntity Create(string name, int horsePower)
    {
        return new EngineEntity()
        {
            Id = Guid.NewGuid(),
            Name = name,
            HorsePower = horsePower
        };
    }
}