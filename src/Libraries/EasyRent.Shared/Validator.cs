using System;
using System.Text;
using System.Text.RegularExpressions;
using EasyRent.Shared.Exceptions;

namespace EasyRent.Shared;

public static class Validator
{
    public static class Number
    {
        public static void InRange(int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    new StringBuilder()
                        .Append("Integer value has to be in range ")
                        .Append($"[{minValue}-{maxValue}]")
                        .ToString());
            }
        }

        public static void InRange(double value, double minValue, double maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    new StringBuilder()
                        .Append("Double value has to be in range ")
                        .Append($"[{minValue}-{maxValue}]")
                        .ToString());
            }
        }

        public static void InRange(decimal value, decimal minValue, decimal maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    new StringBuilder()
                        .Append("Decimal value has to be in range ")
                        .Append($"[{minValue}-{maxValue}]")
                        .ToString());
            }
        }
    }

    public static class Text
    {
        public static void NotNullOrEmpty(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Text cannot be null or empty", nameof(value));
            }
        }

        public static void InRange(string value, int minLength, int maxLength)
        {
            if (value.Length < minLength || value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    new StringBuilder()
                        .Append("Text has to be in length range ")
                        .Append($"[{minLength}-{maxLength}]")
                        .ToString());
            }
        }

        public static void CotainsOnlyLetters(string value)
        {
            if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
            {
                throw new FormatException($"Text should contain only letters");
            }
        }

        public static void ValidateEmail(string value)
        {
            if (!Regex.IsMatch(value, @"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)"))
            {
                throw new FormatException($"Email address: {value} is invalid");
            }
        }

        public static void ValidateUrl(string value)
        {
            if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
            {
                throw new FormatException($"Url address: {value} is invalid");
            }
        }
    }

    public static class Identifiers
    {
        public static void ValidateGuid(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentException($"Guid value: {value} is invalid");
            }
        }
    }

    public static class Date
    {
        public static void AlreadyPassed(DateTime value)
        {
            if (value <= DateTime.Now.Date)
            {
                throw new ResourceExpiredException(value);
            }
        }
        
        public static void IsGreaterThan(DateTime firstDate, DateTime secondDate)
        {
            if (firstDate.Date > secondDate.Date)
            {
                throw new ArgumentOutOfRangeException(nameof(DateTime),
                    $"{firstDate} cannot be bigger than {secondDate}");
            }
        }
    }
}