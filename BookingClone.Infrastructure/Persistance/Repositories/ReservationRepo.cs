using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class ReservationRepo : GenericRepo<Reservation>, IReservationRepo
{
    public ReservationRepo(AppDbContext con) : base(con)
    {
    }

    public override async Task<Reservation?> GetByIdAsync(int id)
    {
        return await con.reservations.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
    }
    public Task<List<Reservation>> GetByDateAsync(DateTime date,
        int pageIdx = 1,
        int pageSize = 3,
        string sortField = "Id",
        string sortType = "asc",
        int? hotelId = null)
    {
        var startDate = date;
        var endDate = startDate.AddDays(1);

        IQueryable<Reservation> query = con.reservations
            .Where(r => r.CheckInDate >= startDate && r.CheckInDate < endDate);

        if (hotelId.HasValue)
            query = query.Where(r => r.Room.HotelId == hotelId);

        query = query.Skip((pageIdx - 1) * pageSize).Take(pageSize);

        if(sortField.ToUpper() == "CHECKINDATE")
        {
            if (sortType.ToUpper() == "ASC")
                query = query.OrderBy(q => q.CheckInDate);

            else query = query.OrderByDescending(q => q.CheckInDate);
        }
        else
        query = query.OrderBy(q => q.Id);

        return query.ToListAsync();
    }

    public async Task<List<Reservation>> GetByUserIdAsync(string userId)
    {
        return await con
            .reservations
            .Where(con => con.UserId == userId)
            .Include(r => r.Room)
            .ToListAsync();
    }
}
