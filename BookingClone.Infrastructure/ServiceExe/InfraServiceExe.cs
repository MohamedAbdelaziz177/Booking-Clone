
using BookingClone.Application.Contracts;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using BookingClone.Infrastructure.Persistance.Repositories;
using BookingClone.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingClone.Infrastructure.ServiceExe;

public static class InfraServiceExe
{
    public static void AddInfraComponents(this IServiceCollection Service, IConfiguration configuration)
    {
        Service.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("cs")));


        Service.AddScoped<IHotelRepo, HotelRepo>();
        Service.AddScoped<IRoomImageRepo, RoomImageRepo>();
        Service.AddScoped<IRoomRepo, RoomRepo>();
        Service.AddScoped<IReservationRepo, ReservationRepo>();
        Service.AddScoped<IFeedbackRepo, FeedbackRepo>();
        Service.AddScoped<IRefreshRokenRepo, RefreshTokenRepo>();
        Service.AddScoped<IUserRepo, UserRepo>();
        Service.AddScoped<IPaymentRepo, PaymentRepo>();
        Service.AddScoped<IUnitOfWork, UnitOfWork>();
       

        Service.AddScoped<IEmailService, EmailService>();
        Service.AddScoped<IStripeService, StripeService>();
        Service.AddScoped<IFileUploadService, FileUploadService>();
        Service.AddScoped<IJwtService, JwtService>();
        
    }
}
