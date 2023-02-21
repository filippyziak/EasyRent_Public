using System.Security.Cryptography;
using System.Text;
using EasyRent.Identity.Domain.DomainServices;
using Konscious.Security.Cryptography;

namespace EasyRent.Identity.Infrastructure.DomainService;

public class Argon2HashProvider : IHashProvider
{
    private const int DegreeOfParallelism = 1;
    private const int Iterations = 2;
    private const int MemorySizeInMb = 5 * ConversionUnit;
    private const int ConversionUnit = 1024;
    private const int PasswordSizeInBytes = 16;

    public byte[] CreateHash(string text, string salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(text))
        {
            Salt = Encoding.UTF8.GetBytes(salt),
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySizeInMb
        };

        return argon2.GetBytes(PasswordSizeInBytes);
    }

    public string CreateSalt()
    {
        var saltBinary = new byte[PasswordSizeInBytes];

        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(saltBinary);

        return Encoding.UTF8.GetString(saltBinary);
    }
}