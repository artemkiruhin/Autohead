namespace AutoHead.Application.Hasher.Base;

public interface IHashService
{
    string Encrypt(string message);
}