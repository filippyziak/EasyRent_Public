namespace EasyRent.Identity.Domain.DomainServices;

public interface IHashProvider
{
    byte[] CreateHash(string text, string salt);
    string CreateSalt();
}