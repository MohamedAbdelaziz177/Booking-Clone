
namespace BookingClone.Application.Common;
public class Result<T> where T : class
{
    public T? Data { get; set; } = null;

    public bool Success { get; set; } = true;

    public string Message { get; set; } = string.Empty;

    public Result(bool  success, string message)
    {
        Success = success;
        Message = message;
    }

}
