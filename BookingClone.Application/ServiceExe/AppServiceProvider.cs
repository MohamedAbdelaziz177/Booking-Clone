using BookingClone.Application.Validators.HotelValidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Mapster;
using BookingClone.Application.Features.Auth;
using BookingClone.Application.Features.Hotel;
using BookingClone.Application.Features.Room;
using BookingClone.Domain.Entities;
using BookingClone.Application.Features.Reservation;
using BookingClone.Application.Contracts;

namespace BookingClone.Application.ServiceExe;

public static class AppServiceProvider
{
    public static void AddAppComponents(this IServiceCollection Service)
    {
       
        Service.AddMapster();

        AuthMapsterAdapter.Configure();
        HotelMapsterAdapter.Configure();
        RoomMapsterAdapter.Configure();
        ReservationMapsterAdapter.Configure();

        Service.AddValidatorsFromAssembly(typeof(CreateHotelCommandValidator).Assembly);
        Service.AddMediatR(typeof(CreateHotelCommandValidator).Assembly);

    
    }

}
