

namespace BookingClone.Application.Features.Auth.Responses;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;

    public DateTime AccessTokenExpiration { get; set; } = default!;

    public DateTime RefreshTokenExpiration { get; set;} = default!;
}
