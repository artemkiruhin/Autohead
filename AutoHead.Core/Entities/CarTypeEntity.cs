namespace AutoHead.Core.Entities;

public class CarTypeEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();

    public static CarTypeEntity Create(string name)
    {
        return new CarTypeEntity()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}