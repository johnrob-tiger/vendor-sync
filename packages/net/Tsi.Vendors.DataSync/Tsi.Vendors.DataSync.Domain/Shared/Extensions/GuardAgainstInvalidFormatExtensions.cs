using System.Text.RegularExpressions;

namespace Tsi.Vendors.DataSync.Domain.Shared.Extensions
{
    public static class GuardAgainstInvalidFormatExtensions
    {
        public static string InvalidFormat(this IGuardClause guardClause, string input, string parameterName,
            string regexPattern, string? message = null)
        {
            var m = Regex.Match(input, regexPattern);

            if (!m.Success || input != m.Value)
            {
                throw new ArgumentException(message ?? $"Input {parameterName} is invalid.", parameterName);
            }

            return input;
        }

        public static T InvalidInput<T>(this IGuardClause guardClause, T input, string parameterName,
            Func<T, bool> predicate, string? message = null)
        {
            if (!predicate(input))
            {
                throw new ArgumentException(message ?? $"Input {parameterName} is invalid.", parameterName);
            }

            return input;
        }

        public static string InvalidEmail(this IGuardClause guardClause, string input, string parameterName,
            string? message = null)
        {
            if (!Regex.IsMatch(input, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                throw new FormatException(message ?? $"{parameterName} is not a valid email address.");
            }

            return input;
        }
    }
}
