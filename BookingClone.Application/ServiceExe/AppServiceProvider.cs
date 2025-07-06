using BookingClone.Application.MappingProfiles;
using BookingClone.Domain.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.ServiceExe;

public static class AppServiceProvider
{
    public static void AddComponents(this IServiceCollection Service)
    {
        Service.AddAutoMapper(typeof(AuthMapper).Assembly);

    }
}
