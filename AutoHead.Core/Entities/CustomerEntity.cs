namespace AutoHead.Core.Entities;

public class CustomerEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }

    public ICollection<OrderEntity> Orders = new List<OrderEntity>();
    
    public static CustomerEntity Create(string name, string email, string phone)
    {
        return new CustomerEntity()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Phone = phone
        };
    }
}