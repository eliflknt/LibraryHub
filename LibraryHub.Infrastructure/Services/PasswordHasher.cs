using System.Security.Cryptography;
using LibraryHub.Application.Interfaces;

namespace LibraryHub.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            KeySize);

        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split('.');

        if (parts.Length != 3)
            return false;

        if (!int.TryParse(parts[0], out int iterations))
            return false;

        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] storedHash = Convert.FromBase64String(parts[2]);

        byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            storedHash.Length);

        return CryptographicOperations.FixedTimeEquals(
            computedHash,
            storedHash);
    }
}