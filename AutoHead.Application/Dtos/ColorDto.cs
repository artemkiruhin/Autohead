namespace AutoHead.Application.Dtos;

public class ColorDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<CarDto> Cars { get; set; } = new List<CarDto>();
}