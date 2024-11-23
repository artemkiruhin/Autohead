using Microsoft.IdentityModel.JsonWebTokens;

namespace AutoHead.Application.Jwt;

public interface IJwtService
{
    string GenerateToken(Guid id, string username);
}