﻿
using BookingClone.Application.Contracts;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.BackgroundJobs;
using BookingClone.Infrastructure.Persistance;
using BookingClone.Infrastructure.Persistance.Repositories;
using BookingClone.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Stripe;

namespace BookingClone.Infrastructure.ServiceExe;

public static class InfraServiceExe
{
    public static void AddInfraComponents(this IServiceCollection Service, IConfiguration configuration)
    {
        Service.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("default"));
        });

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
        Service.AddScoped<ICloudinaryService, CloudinaryService>();
        Service.AddScoped<IJwtService, JwtService>();

        Service.AddScoped<CancelExpiredReservationsJob>();
        Service.AddScoped<PaymentReminderJob>();
        Service.AddScoped<RefundReminderJob>();

        Service.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("redis")!);
        });

        Service.AddScoped<IRedisService, RedisService>();

        StripeConfiguration.ApiKey = configuration["STRIPE:SECRET_KEY"]!;

    }
}
