using AutoHead.Application.Autharization.Base;
using AutoHead.Application.Jwt;
using AutoHead.DataAccess.PostgreSQL.Repositories.Base;

namespace AutoHead.Application.Autharization;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IJwtService _jwtService;

    public AuthService (IEmployeeRepository employeeRepository, IJwtService jwtService)
    {
        _employeeRepository = employeeRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Login(string username, string passwordHash)
    {
        var user = await _employeeRepository.GetByUsername(username);
        if (user is null) throw new KeyNotFoundException($"User with username {username} not found");

        if (user.PasswordHash == passwordHash)
        {
            return _jwtService.GenerateToken(user.Id, username);
        }

        throw new ArgumentException($"User with username {username} and this password not found");
    }
}