using BookingClone.Application.Features.Reservation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Validators.ReservationValidators;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(cmd => cmd.CheckInDate).Must(x => x > DateTime.UtcNow)
            .WithMessage("Enter future CheckIn Date");
        RuleFor(cmd => cmd.CheckOutDate).Must(x => x > DateTime.UtcNow)
             .WithMessage("Enter future CheckOut Date");
        RuleFor(cmd => cmd).Must(x => x.CheckInDate < x.CheckOutDate)
            .WithMessage("CheckIn date must be less than or equal CheckOut");
    }
}
