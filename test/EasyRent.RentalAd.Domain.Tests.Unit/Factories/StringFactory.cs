using System.Linq;

namespace EasyRent.RentalAd.Domain.Tests.Unit.Factories;

public static class StringFactory
{
    public static string GenerateString(int charactersCount)
        => string.Join(string.Empty, Enumerable.Repeat("X", charactersCount));
}