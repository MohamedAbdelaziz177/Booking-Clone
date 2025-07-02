
using BookingClone.Application.Features.Hotel.Commands;
using FluentValidation;

namespace BookingClone.Application.Validators.HotelValidators;

public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
{
    public CreateHotelCommandValidator()
    {
        RuleFor(cmd => cmd.Name).NotEmpty().WithMessage("Name Cannot be Emtpy");
        RuleFor(cmd => cmd.City).NotEmpty().WithMessage("City Cannot be Emtpy");
        RuleFor(cmd => cmd.Phone).NotEmpty().WithMessage("Phone no cannot be empty")
            .Length(11).WithMessage("Enter valid phone number");

        // check if city really exists in country .. To be implemented
        
    }
}
