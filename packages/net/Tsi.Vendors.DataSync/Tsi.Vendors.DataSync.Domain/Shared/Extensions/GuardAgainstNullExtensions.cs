using System.Diagnostics.CodeAnalysis;

namespace Tsi.Vendors.DataSync.Domain.Shared.Extensions
{
    public static partial class GuardAgainstNullExtensions
    {
        public static T Null<T>(this IGuardClause guardClause,[NotNull][ValidatedNotNull] T input, string parameterName, string? message = null)
        {
            if (input is not null) return input;
            
            throw new ArgumentNullException(parameterName, message ?? $"Required input {parameterName} was null.");
        }

        public static string NullOrEmpty(this IGuardClause guardClause, [NotNull] [ValidatedNotNull] string? input,
            string parameterName, string? message = null)
        {
            Guard.Against.Null(input, parameterName, message);

            if (input == string.Empty)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static string NullOrWhiteSpace(this IGuardClause guardClause, [NotNull][ValidatedNotNull] string? input,
            string parameterName, string? message = null)
        {
            Guard.Against.Null(input, parameterName, message);

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static Guid NullOrEmpty(this IGuardClause guardClause, [NotNull] [ValidatedNotNull] Guid? input,
            string parameterName, string? message = null)
        {
            Guard.Against.Null(input, parameterName, message);

            if (input == Guid.Empty)
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input.Value;
        }

        public static IEnumerable<T> NullOrEmpty<T>(this IGuardClause guardClause,
            [NotNull] [ValidatedNotNull] IEnumerable<T>? input,
            string parameterName, string? message = null)
        {
            Guard.Against.Null(input, parameterName, message);

            if (!input.Any())
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }

        public static T Default<T>(this IGuardClause guardClause, [AllowNull, NotNull] T input, string parameterName,
            string? message = null)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)!) || input is null)
            {
                throw new ArgumentException(
                    message ?? $"Parameter {parameterName} is default value for type {typeof(T).Name}", parameterName);
            }

            return input;
        }

        public static T NullOrInvalidInput<T>(this IGuardClause guardClause, T input, string parameterName,
            Func<T, bool> predicate, string? message = null)
        {
            Guard.Against.Null(input, parameterName, message);

            return Guard.Against.InvalidInput(input, parameterName, predicate, message);
        }
    }
}
