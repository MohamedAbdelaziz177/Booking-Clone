using BookingClone.Application.MappingProfiles;
using BookingClone.Application.Validators.HotelValidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Application.Features.Auth.Handlers.CommandHandlers;

namespace BookingClone.Application.ServiceExe;

public static class AppServiceProvider
{
    public static void AddAppComponents(this IServiceCollection Service)
    {
        Service.AddAutoMapper(typeof(HotelMapper).Assembly);
        Service.AddValidatorsFromAssembly(typeof(CreateHotelCommandValidator).Assembly);
        Service.AddMediatR(typeof(CreateHotelCommandValidator).Assembly);
    
    }

}
