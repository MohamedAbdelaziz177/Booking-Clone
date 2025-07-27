
using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Room.Queries;
using MediatR;

namespace BookingClone.Application.Helpers;
public class RedisKeyFactory<T> where T : class
{
    public static string GenerateRedisKey<T>(T request)
    {
        if (request is GetRoomPageQuery RoomPageQuery)
        {
            if (RoomPageQuery != null)
                return MagicValues.ROOM_REDIS_KEY + ": "
                       + RoomPageQuery.PageIdx.ToString() + "-"
                       + RoomPageQuery.PageSize.ToString() + "-"
                       + RoomPageQuery.SortField!.ToString().ToUpper() + "-"
                       + RoomPageQuery.SortType.ToString()!.ToUpper();

            throw new ArgumentException("Invalid request type");
        }
        else if(request is GetHotelPageQuery HotelPageQuery)
        {
            if (HotelPageQuery != null)
                return MagicValues.HOTEL_REDIS_KEY + ": "
                       + HotelPageQuery.PageIdx.ToString() + "- "
                       + HotelPageQuery.PageSize.ToString() + "-"
                       + HotelPageQuery.SortField!.ToString().ToUpper() + "-"
                       + HotelPageQuery.SortType.ToString()!.ToUpper();

            throw new ArgumentException("Invalid request type");
        }
        else if(request is GetRoomByIdQuery RoomIdQuery)
        {
            if (RoomIdQuery != null)
                return MagicValues.ROOM_REDIS_KEY + RoomIdQuery.Id.ToString();
        }
        else if(request is GetHotelByIdQuery HotelIdQuery)
        {
            if(HotelIdQuery != null)
                return MagicValues.HOTEL_REDIS_KEY + HotelIdQuery.id.ToString();
        }

        throw new ArgumentException("Invalid Query Type");
    }

}
