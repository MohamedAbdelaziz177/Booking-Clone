
using BookingClone.Application.Features.Room.Responses;
using Mapster;
using RoomEntity = BookingClone.Domain.Entities.Room;
namespace BookingClone.Application.Features.Room;
public class RoomMapsterAdapter
{
    public static void Configure()
    {

        TypeAdapterConfig<RoomEntity, RoomResponseDto>.NewConfig()
            .Map(dest => dest.Type, src => src.Type.ToString())
            .Map(dest => dest.HotelName, src => src.Hotel.Name)
            .Map(dest => dest.RoomImageDtos, src => src.RoomImages.Select(ri =>
            new RoomImgDto() { ImageId = ri.Id, ImageUrl = ri.ImgUrl }));

    }
}
