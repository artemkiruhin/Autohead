namespace AutoHead.Core.Entities;

public class ColorEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
    
    public static ColorEntity Create(string name)
    {
        return new ColorEntity()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}