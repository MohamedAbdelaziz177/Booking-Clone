
using AutoMapper;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.MappingProfiles;
public class ReservationMapper : Profile
{
    public ReservationMapper() 
    {
        CreateMap<CreateReservationCommand, Reservation>().ReverseMap();

        CreateMap<UpdateReservationCommand, Reservation>().ReverseMap();


    }
}
