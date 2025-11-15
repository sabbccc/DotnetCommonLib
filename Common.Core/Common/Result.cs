namespace Common.Core.Common
{
    public class Result
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }
        public string? ErrorCode { get; private set; }

        private Result(bool success, string? message = null, string? errorCode = null)
        {
            Success = success;
            Message = message;
            ErrorCode = errorCode;
        }

        public static Result Ok(string? message = null) => new Result(true, message);

        public static Result Fail(string? message = null, string? errorCode = null) => new Result(false, message, errorCode);
    }

    public class Result<T>
    {
        public bool Success { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorCode { get; private set; }

        private Result(bool success, T? data = default, string? message = null, string? errorCode = null)
        {
            Success = success;
            Data = data;
            Message = message;
            ErrorCode = errorCode;
        }

        // Success factory
        public static Result<T> Ok(T? data = default, string? message = null)
            => new Result<T>(true, data, message);

        // Failure factory
        public static Result<T> Fail(string? message = null, string? errorCode = null)
            => new Result<T>(false, default, message, errorCode);
    }
}
