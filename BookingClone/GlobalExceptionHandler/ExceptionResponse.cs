using System.Net;

namespace BookingClone.Api.GlobalExceptionHandler
{
    public class ExceptionResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
