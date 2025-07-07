
namespace BookingClone.Application.Common;
public class Result<T> where T : class
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
}
