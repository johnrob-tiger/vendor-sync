namespace Tsi.Vendors.DataSync.Domain.Shared
{
    public class Result<T>
    {
        public Result(bool isFailure, string? error = null)
        {
            IsFailure = isFailure;
            Error = error;
        }

        public Result(T? value)
        {
            Value = value;
        }

        public bool IsFailure { get; }

        public string? Error { get; }

        public T? Value { get; }

        public static Result<T> Success(T? value = default)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(true, error);
        }
    }
}
