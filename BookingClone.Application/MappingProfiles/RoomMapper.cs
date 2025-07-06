
using AutoMapper;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.MappingProfiles;
public class RoomMapper : Profile
{
    public RoomMapper() 
    {
        CreateMap<Room, CreateRoomCommand>().ReverseMap();

        CreateMap<Room, UpdateRoomCommand>().ReverseMap();

        CreateMap<Room, RoomResponseDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
            .ForMember(dest => dest.RoomImageDtos, opt =>
            {
                opt.MapFrom(src => src.RoomImages.Select(ri =>
                new RoomImgDto() { ImageId = ri.Id, ImageUrl = ri.ImgUrl}).ToList());
            })
            .ReverseMap();

        
    }
}
