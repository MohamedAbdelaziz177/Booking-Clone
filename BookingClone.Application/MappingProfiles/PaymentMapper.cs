
using AutoMapper;
using BookingClone.Application.Features.Payment.Commands;
using BookingClone.Domain.Entities;

namespace BookingClone.Application.MappingProfiles;
public class PaymentMapper : Profile
{
    public PaymentMapper() 
    {
        CreateMap<Payment, CreatePaymentCommand>().ReverseMap();
    }
}
