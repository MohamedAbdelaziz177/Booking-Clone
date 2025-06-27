
using AutoMapper;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.MappingProfiles;

public class HotelMapper : Profile
{
    public HotelMapper(){

        CreateMap<Hotel, CreateHotelCommand>().ReverseMap();

        CreateMap<Hotel, UpdateHotelCommand>().ReverseMap();

        CreateMap<Hotel, HotelResponseDto>()
            .ReverseMap();

    }

}
