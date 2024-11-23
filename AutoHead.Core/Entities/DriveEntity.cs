namespace AutoHead.Core.Entities;

public class DriveEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
    
    public static DriveEntity Create(string name)
    {
        return new DriveEntity()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}