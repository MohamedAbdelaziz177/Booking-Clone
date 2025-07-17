
using BookingClone.Application.Features.Auth.Responses;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.Contracts;

public interface IJwtService
{
    Task<string> GenerateAccessTokenAsync(User user);
    Task<TokenResponseDto> GetTokensAsync(string userId);
    Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
}
