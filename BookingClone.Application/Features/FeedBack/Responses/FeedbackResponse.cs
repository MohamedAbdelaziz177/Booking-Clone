
namespace BookingClone.Application.Features.FeedBack.Responses;
public class FeedbackResponse
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public double Rating { get; set; }

    public string? Comment { get; set; }
 
}
