using System.ComponentModel;

namespace Tsi.Vendors.DataSync.Domain.Shared.Extensions
{
    public static class GuardAgainstOutOfRangeExtensions
    {
        public static int EnumOutOfRange<T>(this IGuardClause guardClause, int input, string parameterName,
            string? message = null) where T : struct, Enum
        {
            if (Enum.IsDefined(typeof(T), input)) return input;

            if (string.IsNullOrEmpty(message))
            {
                throw new InvalidEnumArgumentException(parameterName, input, typeof(T));
            }

            throw new InvalidEnumArgumentException(message);
        }

        public static T EnumOutOfRange<T>(this IGuardClause guardClause, T input, string parameterName,
            string? message = null) where T : struct, Enum
        {
            if (Enum.IsDefined(typeof(T), input)) return input;

            if (string.IsNullOrEmpty(message))
            {
                throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(input), typeof(T));
            }

            throw new InvalidEnumArgumentException(message);
        }

        public static IEnumerable<T> OutOfRange<T>(this IGuardClause guardClause, IEnumerable<T> input,
            string parameterName, T rangeFrom, T rangeTo, string? message = null) where T : IComparable, IComparable<T>
        {
            if (rangeFrom.CompareTo(rangeTo) > 0)
            {
                throw new ArgumentException(
                    message ?? $"{nameof(rangeFrom)} should be less than or equal to {nameof(rangeTo)}.",
                    parameterName);
            }

            var arr = input as T[] ?? input.ToArray();

            if (!arr.Any(x => x.CompareTo(rangeFrom) < 0 || x.CompareTo(rangeTo) > 0)) return arr;

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} had out of range items.");
            }

            throw new ArgumentOutOfRangeException(message);

        }

        public static DateTime OutofSQLDateRange(this IGuardClause guardClause, DateTime input, string parameterName,
            string? message = null)
        {
            const long sqlMinDateTicks = 552877920000000000;
            const long sqlMaxDateTicks = 3155378975999970000;

            return OutOfRange<DateTime>(guardClause, input, parameterName!, new DateTime(sqlMinDateTicks), new DateTime(sqlMaxDateTicks), message);
        }

        public static T OutOfRange<T>(this IGuardClause guardClause, T input, string parameterName, T rangeFrom,
            T rangeTo, string? message = null) where T : IComparable, IComparable<T>
        {
            if (rangeFrom.CompareTo(rangeTo) > 0)
            {
                throw new ArgumentException(
                    message ?? $"{nameof(rangeFrom)} should be less than or equal to {nameof(rangeTo)}.",
                    parameterName);
            }

            if (input.CompareTo(rangeFrom) >= 0 && input.CompareTo(rangeTo) <= 0) return input;

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Input {parameterName} was out of range.");
            }

            throw new ArgumentOutOfRangeException(parameterName, message);
        }

        public static string OutOfRange(this IGuardClause guardClause, string input, string parameterName,
            int lengthFrom, int lengthTo, string? message = null)
        {
            Guard.Against.NullOrEmpty(input, parameterName, message);

            if (input.Length < lengthFrom)
            {
                throw new ArgumentOutOfRangeException(parameterName, message ?? $"Input {parameterName} should be greater than or equal to {lengthFrom}.");
            }

            if (input.Length > lengthTo)
            {
                throw new ArgumentOutOfRangeException(parameterName, message ?? $"Input {parameterName} should be less than or equal to {lengthTo}.");
            }

            return input;
        }
    }
}
