namespace AutoHead.Application.Autharization.Base;

public interface IAuthService
{
    Task<string> Login(string username, string passwordHash);
}