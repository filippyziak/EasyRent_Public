namespace EasyRent.NetCore.HttpContext;

public interface IReadOnlyHttpAccessor
{
    string CurrentUserId { get; }
    string CurrentUserEmailAddress { get; }
    string CurrentUserAccountType { get; }
}