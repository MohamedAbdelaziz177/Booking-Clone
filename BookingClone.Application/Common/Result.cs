
namespace BookingClone.Application.Common;
public class Result<T> 
{
    public T? Data { get; set; }

    public bool Success { get; set; } = true;

    public string Message { get; set; } = string.Empty;

    public Result(bool  success, string message)
    {
        Success = success;
        Message = message;
    }

    public Result(T? data, bool success = true, string message = "")
    {
        Data = data;
        Success = success;
        Message = message;
    }

    public static Result<T> CreateSuccessResult(T? data, bool success = true)
    {
        return new Result<T>(data, success);
    }

    public static Result<T> CreateFailuteResult(string msg, bool success = false)
    {
        return new Result<T>(false, msg);
    }
}
