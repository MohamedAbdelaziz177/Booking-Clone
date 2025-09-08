
using BookingClone.Application.Features.Auth.Commands;
using FluentValidation;

namespace BookingClone.Application.Validators.AuthValidators;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email musn't be empty")
            .EmailAddress().WithMessage("Enter valid email address");

        RuleFor(c => c.token)
            .NotEmpty().WithMessage("Token Cannot be empty");
    }
}
