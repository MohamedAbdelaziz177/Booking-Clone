
namespace BookingClone.Application.Features.Payment.Responses;
public class StripeResponseDto
{
    public string IntentId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = default!;
}
