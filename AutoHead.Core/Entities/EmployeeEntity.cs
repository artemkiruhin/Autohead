namespace AutoHead.Core.Entities;

public class EmployeeEntity
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime Hired { get; set; }
    
    public static EmployeeEntity Create(string username, string passwordHash)
    {
        return new EmployeeEntity()
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = passwordHash,
            Hired = DateTime.UtcNow
        };
    }
}