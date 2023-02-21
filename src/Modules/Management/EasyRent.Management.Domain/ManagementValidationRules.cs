namespace EasyRent.Management.Domain;

public static class ManagementValidationRules
{
    public static class PlaceFeature
    {
        public static class Description
        {
            public const int MinLength = 5;
            public const int MaxLength = 100;
        }
    }
}