
using BookingClone.Application.Features.Auth.Responses;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.Contracts;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken(string refreshToken);

    TokenResponseDto GetTokens(User user);

    bool ValidateRefreshToken(string refreshToken);
}
