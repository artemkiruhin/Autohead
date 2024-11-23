namespace AutoHead.Application.Dtos;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}