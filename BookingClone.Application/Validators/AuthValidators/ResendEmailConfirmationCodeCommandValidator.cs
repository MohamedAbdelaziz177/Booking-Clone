
using BookingClone.Application.Features.Auth.Commands;
using FluentValidation;

namespace BookingClone.Application.Validators.AuthValidators;

public class ResendEmailConfirmationCodeCommandValidator : AbstractValidator<ResendConfirmationCodeCommand>
{
    public ResendEmailConfirmationCodeCommandValidator()
    {
        RuleFor(recc => recc.Email)
            .NotEmpty().WithMessage("Email field cannot be empty")
            .EmailAddress().WithMessage("Enter Valid email address");
    }
}
