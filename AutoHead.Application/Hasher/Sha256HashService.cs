using System.Security.Cryptography;
using System.Text;
using AutoHead.Application.Hasher.Base;

namespace AutoHead.Application.Hasher;

public class Sha256HashService : IHashService
{
    public string Encrypt(string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentException("Message cannot be null or empty");

        var messageBytes = Encoding.UTF8.GetBytes(message);

        using var sha256 = SHA256.Create();

        var hashBytes = sha256.ComputeHash(messageBytes);

        var builder = new StringBuilder();
        foreach (var t in hashBytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }
}