using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Auth.Responses;
using BookingClone.Domain.Entities;

namespace BookingClone.Infrastructure.Services;

public class JwtService : IJwtService
{
    public string GenerateAccessToken(User user)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public TokenResponseDto GetTokens(User user)
    {
        throw new NotImplementedException(); 
    }

    public bool ValidateRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
