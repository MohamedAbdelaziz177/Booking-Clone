
using BookingClone.Api.GlobalExceptionHandler;
using BookingClone.Api.ServiceExe;
using BookingClone.Application.ServiceExe;
using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Persistance;
using BookingClone.Infrastructure.ServiceExe;
using CloudinaryDotNet;
using DotNetEnv;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections;
using System.Reflection;
using System.Text;

namespace BookingClone.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
        
            builder.Services.AddInfraComponents(builder.Configuration);
            builder.Services.AddAppComponents();
            builder.Services.AddApiComponents(builder.Configuration);

            foreach (var (key, value) in Environment.GetEnvironmentVariables().Cast<DictionaryEntry>())
            {
                builder.Configuration[key.ToString()!] = value?.ToString();
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.MapControllers();

            app.Run();
        }
    }
}
