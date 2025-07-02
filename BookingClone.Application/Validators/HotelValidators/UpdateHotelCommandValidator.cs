

using BookingClone.Application.Features.Hotel.Commands;
using FluentValidation;

namespace BookingClone.Application.Validators.HotelValidators;

public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
{
    public UpdateHotelCommandValidator()
    {
        RuleFor(cmd => cmd.Name).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(cmd => cmd.City).NotEmpty().WithMessage("City Cannot be Emtpy");

        RuleFor(cmd => cmd.Phone).NotEmpty().WithMessage("Phone no cannot be empty")
            .Length(11).WithMessage("Enter valid phone number");
    }
}
