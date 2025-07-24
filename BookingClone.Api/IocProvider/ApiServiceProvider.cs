using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Persistance;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;

namespace BookingClone.Api.ServiceExe
{
    public static class ApiServiceProvider
    {
        public static void AddApiComponents(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddIdentity<User, IdentityRole>()
                 .AddEntityFrameworkStores<AppDbContext>();

            Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Booking Clone_",
                    Description = "Rest API for a mock hotel reservation system",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Email = "mohamecabelaziz66@gmail.com",
                        Name = "Mohamed Abdelaziz"
                    }
                }
                );

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header

                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()

                    }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });


            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!))

                    };
                });

            Services.AddHangfire(config =>
            config.UseSqlServerStorage(configuration.GetConnectionString("default")));

            Services.AddHangfireServer();
        }

        public static void AddRateLimitters(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddSlidingWindowLimiter("SlidingWindow", opts =>
                {
                    opts.Window = TimeSpan.FromSeconds(30);
                    opts.PermitLimit = 5;
                    opts.QueueLimit = 2;
                    opts.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opts.SegmentsPerWindow = 3;
                });

                options.AddTokenBucketLimiter("TokensBucket", opts =>
                {
                    opts.TokenLimit = 5;
                    opts.TokensPerPeriod = 2;
                    opts.ReplenishmentPeriod = TimeSpan.FromSeconds(40);
                    opts.AutoReplenishment = true;
                });
            });
        }
    }
}
