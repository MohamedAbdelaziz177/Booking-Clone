
using AutoMapper;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;

namespace BookingClone.Application.MappingProfiles;
public class ReservationMapper : Profile
{
    public ReservationMapper() 
    {
        CreateMap<Reservation, CreateReservationCommand>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Reservation, UpdateReservationCommand>().ReverseMap();

        CreateMap<Reservation, ReservationResponseDto>()
            .ForMember(dest => dest.ReservationStatus,
            opt => opt.MapFrom(src => src.ReservationStatus.ToString()))
            .ReverseMap();
    }
}
