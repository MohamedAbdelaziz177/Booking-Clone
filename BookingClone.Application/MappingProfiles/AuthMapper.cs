
using AutoMapper;
using BookingClone.Application.Features.Auth.Commands;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.MappingProfiles;

public class AuthMapper : Profile
{
    public AuthMapper() 
    {
        CreateMap<User, RegisterCommand>()
            .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
            .ReverseMap();
    }
}
