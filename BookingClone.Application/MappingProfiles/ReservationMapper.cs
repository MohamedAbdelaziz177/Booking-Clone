
using AutoMapper;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.MappingProfiles;
public class ReservationMapper : Profile
{
    public ReservationMapper() 
    {
        CreateMap<Reservation, CreateReservationCommand>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Reservation, UpdateReservationCommand>().ReverseMap();

    }
}
