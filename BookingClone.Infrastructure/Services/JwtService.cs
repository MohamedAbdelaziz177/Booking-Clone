using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Responses;
using BookingClone.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingClone.Domain.IRepositories;
using BookingClone.Application.Exceptions;
using Microsoft.VisualBasic;
using BookingClone.Application.Common;

namespace BookingClone.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;
    private readonly UserManager<User> userManager;
    private readonly IHostEnvironment environment;
    private readonly IUnitOfWork unitOfWork;

    public JwtService(IConfiguration configuration,
        UserManager<User> userManager,
        IHostEnvironment environment,
        IUnitOfWork unitOfWork)
    {
        this.configuration = configuration;
        this.userManager = userManager;
        this.environment = environment;
        this.unitOfWork = unitOfWork;
    }

    private DateTime GetTokenExpiration()
    {
        return !environment.IsProduction() ? DateTime.UtcNow.AddDays(1) 
            : DateTime.UtcNow.AddMinutes(MagicValues.ACCESS_TOKEN_LIFE_TIME_MINS);
    }
    public async Task<string> GenerateAccessTokenAsync(User user)
    {

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())

        };

        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));


        JwtSecurityToken Token = new JwtSecurityToken(

            issuer: configuration["JWT:Issuer"],

            audience: configuration["JWT:Audience"],

            expires: GetTokenExpiration(),

            claims: claims,

            signingCredentials: new SigningCredentials(

            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!)),
            SecurityAlgorithms.HmacSha256

            )

            );

        return new JwtSecurityTokenHandler().WriteToken(Token);
    }

    
    private async Task<RefreshToken> GenerateRefreshTokenAsync(User user)
    {
        RefreshToken refreshToken = new RefreshToken();

        refreshToken.UserId = user.Id;
        refreshToken.IsRevoked = false;
        refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(MagicValues.REFRESH_TOKEN_LIFE_TIME_DAYS);

        refreshToken.Token = Guid.NewGuid().ToString() + "_" + Guid.NewGuid().ToString();

        await unitOfWork.RefreshRokenRepo.AddAsync(refreshToken);

        return refreshToken;
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(string userId)
    {
        User? user = await userManager.FindByIdAsync(userId);

        if(user == null)
            throw new EntityNotFoundException("No such user exists");

        return await this.GenerateRefreshTokenAsync(user);
    }

    
    //public async Task<(string token, bool refreshed)> GenerateRefreshToken(string refreshToken)
    //{
    //    var ValidationRes = ValidateRefreshToken(refreshToken);
    //
    //    if (!ValidationRes.valid)
    //        return ("", false);
    //
    //    RefreshToken token = await GetRefreshToken(ValidationRes.refToken.UserId);
    //
    //    return (token.Token, true);
    //}

    private async Task<TokenResponseDto> GetTokensAsync(User user)
    {
        RefreshToken refreshToken = await this.GenerateRefreshTokenAsync(user);
        return new TokenResponseDto()
        {
            AccessToken = await this.GenerateAccessTokenAsync(user),
            RefreshToken = refreshToken.Token,
            AccessTokenExpiration = this.GetTokenExpiration(),
            RefreshTokenExpiration = refreshToken.ExpiryDate
        };
    }

    public async Task<TokenResponseDto> GetTokensAsync(string userId)
    {
        User? user = await userManager.FindByIdAsync(userId);

        if (user == null)
            throw new EntityNotFoundException("No such user exists");

        return await GetTokensAsync(user);
    }

    public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var ValidationRes = await ValidateRefreshTokenAsync(refreshToken);

        if (!ValidationRes.valid)
            throw new RefreshTokenNotValidException("Refresh Token is not valid");

        return await GetTokensAsync(ValidationRes.refToken.UserId);

    }

    private async Task<(bool valid, RefreshToken refToken)> ValidateRefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return (false, null!);

        RefreshToken? token = await unitOfWork.RefreshRokenRepo.GetByTokenAsync(refreshToken);

        if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.Now)
            return (false, null!);

        return (true, token);
    }
}


/*

--- (o_o) full process abstraction (o_o) --- 

New User ? ---> call public GetTokens (userId)
User with expired access token ? ->> call public RefreshToken(ref-token)

all the rest are just helper methods o_o :( 
 
 */
