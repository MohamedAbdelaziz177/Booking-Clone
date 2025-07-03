
namespace BookingClone.Application.Features.Payment.Responses;
public class StripeResponseDto
{
    public string SessionId { get; set; } = string.Empty;
    
    public string IntentId { get; set; } = string.Empty;

    public string SessionUrl { get; set; } = string.Empty;
}
