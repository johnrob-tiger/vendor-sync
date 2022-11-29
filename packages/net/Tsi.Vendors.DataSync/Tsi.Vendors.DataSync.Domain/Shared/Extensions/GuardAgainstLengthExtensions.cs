namespace Tsi.Vendors.DataSync.Domain.Shared.Extensions
{
    public static class GuardAgainstLengthExtensions
    {
        public static string MaxLength(this IGuardClause guardClause, string input, int maxLength, string parameterName,
            string? message = null)
        {
            Guard.Against.NullOrEmpty(input, parameterName, message);

            if (input.Length > maxLength)
            {
                throw new ArgumentException(message ?? $"Input {parameterName} exceeds the max length of {maxLength}.",
                    parameterName);
            }

            return input;
        }

        public static string MinLength(this IGuardClause guardClause, string input, int minLength, string parameterName,
            string? message = null)
        {
            Guard.Against.NullOrEmpty(input, parameterName, message);

            if (input.Length < minLength)
            {
                throw new ArgumentException(message ?? $"Input {parameterName} is less than the min length of {minLength}.",
                    parameterName);
            }

            return input;
        }
    }
}
