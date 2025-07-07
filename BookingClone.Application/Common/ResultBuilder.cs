
namespace BookingClone.Application.Common;
internal static class ResultBuilder<T>  where T : class
{
    public static Result<T> CreateFailureResponse(string message, bool success = false)
    {
        return new Result<T>(success, message);
    }

    public static Result<T> CreateSuccessResponse(T data,
        bool success = true,
        string message = null)
    {
        Result<T> result = new Result<T>(success, message);
        result.Data = data;

        return result;
    }

    public static Result<T> CreateSuccessResponse(string message, bool success = true)
    {
        return new Result<T>(success, message);
    }
}
