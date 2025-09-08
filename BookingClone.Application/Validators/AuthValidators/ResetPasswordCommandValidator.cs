
using BookingClone.Application.Features.Auth.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace BookingClone.Application.Validators.AuthValidators;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(cmd => cmd)
            .Must(cmd => cmd.Password == cmd.ConfirmPassword)
            .WithMessage("Password and confirm password are not equal");

        RuleFor(cmd => cmd.email)
            .NotEmpty().WithMessage("Email field must be provided")
            .EmailAddress().WithMessage("Email is not valid email address"); 
            
    }
}
