

using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;
using Mapster;

namespace BookingClone.Application.Features.Auth;

public static class AuthMapsterAdapter
{
    public static void Configure()
    {
        TypeAdapterConfig<RegisterCommand, User>.NewConfig()
            .Map(dest => dest.UserName, src => src.Email.Substring(0, src.Email.IndexOf('@')));
           
    }
}
