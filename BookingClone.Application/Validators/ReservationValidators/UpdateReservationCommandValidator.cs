
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Domain.IRepositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace BookingClone.Application.Validators.ReservationValidators;

public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
{
    private readonly IReservationRepo reservationRepo;

    public UpdateReservationCommandValidator(IReservationRepo reservationRepo)
    {
        this.reservationRepo = reservationRepo;

        RuleFor(r => r.Id).MustAsync(async (id, ct) => await CheckNotChangedId(id));

        RuleFor(cmd => cmd.CheckInDate).Must(x => x > DateTime.UtcNow)
            .WithMessage("Enter future CheckIn Date");
        RuleFor(cmd => cmd.CheckOutDate).Must(x => x > DateTime.UtcNow)
             .WithMessage("Enter future CheckOut Date");
        RuleFor(cmd => cmd).Must(x => x.CheckInDate < x.CheckOutDate)
            .WithMessage("CheckIn date must be less than or equal CheckOut");


    }

    private async Task<bool> CheckNotChangedId(int id)
    {
        var room = await reservationRepo.GetByIdAsync(id);

        return room != null;
    }
}
