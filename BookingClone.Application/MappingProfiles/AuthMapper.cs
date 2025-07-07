
//using AutoMapper;
//using BookingClone.Application.Features.Auth.Commands;
//using BookingClone.Domain.Entities;
//
//namespace BookingClone.Application.MappingProfiles;
//
//public class AuthMapper : Profile
//{
//    public AuthMapper() 
//    {
//        CreateMap<RegisterCommand, User>()
//            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
//            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
//            .ForAllOtherMembers(opt => opt.Ignore());
//    }
//}
