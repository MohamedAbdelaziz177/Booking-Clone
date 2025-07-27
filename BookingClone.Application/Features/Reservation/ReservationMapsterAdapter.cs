
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Responses;
using Mapster;
using ReservationEntity = BookingClone.Domain.Entities.Reservation;

namespace BookingClone.Application.Features.Reservation;

public static class ReservationMapsterAdapter
{
    public static void Configure()
    {
        TypeAdapterConfig<ReservationEntity, ReservationResponseDto>.NewConfig()
            .Map(dest => dest.ReservationStatus, src => src.ReservationStatus.ToString())
            .Map(dest => dest.CheckInDate, src => src.CheckInDate.ToLocalTime())
            .Map(dest => dest.CheckOutDate, src => src.CheckOutDate.ToLocalTime())
            .Map(dest => dest.RoomDetails, src => src.Room);
            //.Map(dest => dest.RoomDetails.RoomNumber, src => src.Room.RoomNumber)
            //.Map(dest => dest.RoomDetails.Id, src => src.Room.Id)
            //.Map(dest => dest.RoomDetails.PricePerNight, src => src.Room.PricePerNight);
    }
}
