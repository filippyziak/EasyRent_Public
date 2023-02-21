using EasyRent.Shared.Exceptions;

namespace EasyRent.Identity.Domain.Exceptions;

public class InvalidCredentialsException : BaseException
{
    private const string CustomMessage = "Invalid user account credentials";

    public override string ErrorCode => "InvalidCredentials";

    public InvalidCredentialsException(string message = CustomMessage) : base(message)
    {
    }
}