
using BookingClone.Application.Features.Auth.Commands;
using FluentValidation;

namespace BookingClone.Application.Validators.AuthValidators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(rc => rc.Email)
            .EmailAddress()
            .WithMessage("Email is not valid");

        RuleFor(rc => rc).Must(rc => rc.Password == rc.ConfirmPassword)
            .WithMessage("Password And ConfirmPassword don't match");

        RuleFor(rc => rc.FirstName)
            .NotEmpty().WithMessage("FName cannot be less than 4 characters")
            .MinimumLength(4)
            .WithMessage("FName cannot be less than 4 characters")
            .MaximumLength(15)
            .WithMessage("FName cannot exceed 15 characters");



        RuleFor(rc => rc.LastName)
            .MinimumLength(4)
            .WithMessage("LName cannot be less than 4 characters")
            .MaximumLength(15)
            .WithMessage("FName cannot exceed 15 characters");
    }
}
