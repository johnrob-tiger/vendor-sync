namespace Tsi.Vendors.DataSync.Domain.Shared.Extensions
{
    public static class GuardAgainstZeroExtensions
    {
        public static int Zero(this IGuardClause guardClause, int input, string parameterName, string? message = null)
        {
            return Zero<int>(guardClause, input, parameterName, message);
        }

        public static int ZeroOrLess(this IGuardClause guardClause, int input, string parameterName, string? message = null)
        {
            return NegativeOrZero<int>(guardClause, input, parameterName, message);
        }

        public static long Zero(this IGuardClause guardClause, long input, string parameterName, string? message = null)
        {
            return Zero<long>(guardClause, input, parameterName, message);
        }

        public static long ZeroOrLess(this IGuardClause guardClause, long input, string parameterName, string? message = null)
        {
            return NegativeOrZero<long>(guardClause, input, parameterName, message);
        }

        public static decimal Zero(this IGuardClause guardClause, decimal input, string parameterName, string? message = null)
        {
            return Zero<decimal>(guardClause, input, parameterName, message);
        }

        public static decimal ZeroOrLess(this IGuardClause guardClause, decimal input, string parameterName, string? message = null)
        {
            return NegativeOrZero<decimal>(guardClause, input, parameterName, message);
        }

        public static double Zero(this IGuardClause guardClause, double input, string parameterName, string? message = null)
        {
            return Zero<double>(guardClause, input, parameterName, message);
        }

        public static double ZeroOrLess(this IGuardClause guardClause, double input, string parameterName, string? message = null)
        {
            return NegativeOrZero<double>(guardClause, input, parameterName, message);
        }

        public static float Zero(this IGuardClause guardClause, float input, string parameterName, string? message = null)
        {
            return Zero<float>(guardClause, input, parameterName, message);
        }

        public static float ZeroOrLess(this IGuardClause guardClause, float input, string parameterName, string? message = null)
        {
            return NegativeOrZero<float>(guardClause, input, parameterName, message);
        }

        public static TimeSpan Zero(this IGuardClause guardClause, TimeSpan input, string parameterName,
            string? message = null)
        {
            return Zero<TimeSpan>(guardClause, input, parameterName, message);
        }

        private static T Zero<T>(this IGuardClause guardClause, T input, string parameterName, string? message = null)
            where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} cannot be zero.", parameterName);
            }

            return input;
        }

        private static T NegativeOrZero<T>(this IGuardClause guardClause, T input, string parameterName,
            string? message = null) where T : struct, IComparable
        {
            if (input.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} must be greater than zero.",
                    parameterName);
            }

            return input;
        }
    }
}
