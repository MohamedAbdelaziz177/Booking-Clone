
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
            .Map(dest => dest.RoomDetails, src => src.Room);
    }
}
