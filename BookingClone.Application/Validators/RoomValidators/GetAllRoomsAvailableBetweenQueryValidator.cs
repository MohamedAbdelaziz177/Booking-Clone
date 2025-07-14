
using BookingClone.Application.Features.Room.Queries;
using FluentValidation;

namespace BookingClone.Application.Validators.RoomValidators;

public class GetAllRoomsAvailableBetweenQueryValidator : AbstractValidator<GetAllRoomsAvailableBetweenQuery>
{
    public GetAllRoomsAvailableBetweenQueryValidator()
    {
        RuleFor(q => q).Must(q => q.minPrice <= q.maxPrice)
            .WithMessage("min price must be less than max price");

        RuleFor(q => q.SortType)
            .Must(v => v.ToUpper() == "ASC" || v.ToUpper() == "DESC")
            .WithMessage("SortType must be either 'ASC' or 'DESC'.");

        RuleFor(q => q).Must(q => q.start < q.end)
            .WithMessage("checkIn date must be less than checkout date");
       

    }
}
