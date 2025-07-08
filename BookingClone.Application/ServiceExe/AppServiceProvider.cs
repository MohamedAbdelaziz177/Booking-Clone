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

namespace BookingClone.Application.ServiceExe;

public static class AppServiceProvider
{
    public static void AddAppComponents(this IServiceCollection Service)
    {
        //Service.AddAutoMapper(opt =>
        //{
        //    opt.AddProfile<HotelMapper>();
        //    opt.AddProfile<AuthMapper>();
        //});

        Service.AddMapster();

        AuthMapsterAdapter.Configure();
        HotelMapsterAdapter.Configure();
        RoomMapsterAdapter.Configure();
        ReservationMapsterAdapter.Configure();

        Service.AddValidatorsFromAssembly(typeof(CreateHotelCommandValidator).Assembly);
        Service.AddMediatR(typeof(CreateHotelCommandValidator).Assembly);
    
    }

}
