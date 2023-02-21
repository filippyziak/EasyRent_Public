namespace EasyRent.Identity.Shared.Constants;

public class AuthorizationPolicies
{
    public static class ModeratorPolicy
    {
        public static string Role => "Moderator";
    }

    public static class TenantPolicy
    {
        public static string Role => "Tenant";
    }

    public static class PlaceOwnerPolicy
    {
        public static string Role => "PlaceOwner";
    }
    
    public static class SuspendPolicy
    {
        public static string Claim => "State";
        public static string Suspended => "Suspended";
    }
}