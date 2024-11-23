namespace AutoHead.Application.Dtos;

public class EngineDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int HorsePower { get; set; }
    public List<CarDto> Cars { get; set; } = new List<CarDto>();
}