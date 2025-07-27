using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using FluentValidation;
using System.Net;

namespace BookingClone.Api.GlobalExceptionHandler
{
    public class GlobalExceptionMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> Logger;
        private readonly RequestDelegate next;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> Logger,
            RequestDelegate Next)
        {
            this.Logger = Logger;
            next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Exception occured");

                ExceptionResponse response = HandleException(ex);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)response.StatusCode;
                await context.Response.WriteAsJsonAsync(Result<ExceptionResponse>.CreateFailuteResult(ex.Message));
            }
        }

        private ExceptionResponse HandleException(Exception ex)
        {
            ExceptionResponse response = new ExceptionResponse();
            Type type = ex.GetType();

            if (type == typeof(EntityNotFoundException))
                response.StatusCode = HttpStatusCode.NotFound;

            else if (type == typeof(LoginFailedException)
                || type == typeof(RegistrationFailedException)
                || type == typeof(OtpNotValidException)
                || type == typeof(UnavailablePaymentException)
                || type == typeof(AlreadyPaidException)
                || type == typeof(ValidationException)
                )
                response.StatusCode = HttpStatusCode.BadRequest;

            else if (type == typeof(RefreshTokenNotValidException))
                response.StatusCode=HttpStatusCode.Unauthorized;

            else
                response = new ExceptionResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };

            response.Description = ex.Message;

            return response;
        }
    }
}
