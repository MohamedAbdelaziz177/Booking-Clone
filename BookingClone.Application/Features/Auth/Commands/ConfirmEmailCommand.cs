
using BookingClone.Application.Common;
using BookingClone.Application.Features.Auth.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BookingClone.Application.Features.Auth.Commands;

public class ConfirmEmailCommand : IRequest<Result<TokenResponseDto>>
{
    public string Email { get; set; } = string.Empty;

    public string token { get; set; } = string.Empty;
}
