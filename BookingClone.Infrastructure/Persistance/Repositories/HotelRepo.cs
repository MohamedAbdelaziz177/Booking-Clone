using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class HotelRepo : GenericRepo<Hotel>, IHotelRepo
{
    public HotelRepo(AppDbContext con) : base(con)
    {
    }

    public override async Task<List<Hotel>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        )
    {
        var query = con.hotels.Skip((pageIdx - 1) * pageSize).Take(pageSize);

        if (sortField.ToUpper() == "NAME")
        {
            if (sortDir.ToUpper() == "DESC")
                query = query.OrderByDescending(x => x.Name);

            else query = query.OrderBy(x => x.Name);
        }

        else query = query.OrderByDescending(x => x.Id);


        return await query.ToListAsync();
    }

    public async Task<List<Hotel>> GetByCityAsync(string city)
    {
        return await this.Filter(x => x.City.ToUpper() == city.ToUpper());
    }

    public async Task<List<Hotel>> GetByCountryAsync(string country)
    {
        return await this.Filter(x => x.Country.ToUpper() == country.ToUpper());
    }
}
