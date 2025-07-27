
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Enums;
using Mapster;
using RoomEntity = BookingClone.Domain.Entities.Room;
namespace BookingClone.Application.Features.Room;
public class RoomMapsterAdapter
{
    public static void Configure()
    {

        TypeAdapterConfig<CreateRoomCommand, RoomEntity>.NewConfig()
            .Map(dest => dest.Type, src => Enum.Parse<RoomType>(src.Type));

        TypeAdapterConfig<UpdateRoomCommand, RoomEntity>.NewConfig()
            .Map(dest => dest.Type, src => Enum.Parse<RoomType>(src.Type));

        TypeAdapterConfig<RoomEntity, RoomResponseDto>.NewConfig()
            .Map(dest => dest.Type, src => src.Type.ToString())
            .Map(dest => dest.HotelName, src => src.Hotel.Name)
            .Map(dest => dest.RoomImageDtos, src => src.RoomImages.Select(ri =>
            new RoomImgDto() { ImageId = ri.Id, ImageUrl = ri.ImgUrl }));

        TypeAdapterConfig<RoomEntity, RoomCardResponse>.NewConfig();

    }
}
