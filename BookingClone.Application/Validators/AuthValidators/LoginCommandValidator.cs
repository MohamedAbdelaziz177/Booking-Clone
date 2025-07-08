
using BookingClone.Application.Features.Auth.Commands;
using FluentValidation;

namespace BookingClone.Application.Validators.AuthValidators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(lc => lc.Email)
            .NotEmpty().WithMessage("Email field cannot be empty")
            .EmailAddress().WithMessage("Not valid email address");
    }
}
