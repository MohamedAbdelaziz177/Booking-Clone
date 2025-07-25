using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
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

    public async Task<List<Reservation>> GetExpiredReservations()
    {
        DateTime SupposedCheckIn = DateTime.UtcNow.AddDays(-1);

        return await con
            .reservations
            .Where(r =>
             r.CheckInDate <= SupposedCheckIn &&
             r.ReservationStatus == ReservationStatus.Pending ).ToListAsync();
    }

    public async Task CommitCheckIn(int reservationId)
    {
        Reservation? reservation = await con.reservations.FirstOrDefaultAsync(r => r.Id ==  reservationId);

        reservation.ReservationStatus = ReservationStatus.CheckedIn;

        await con.SaveChangesAsync();

    }

    public async Task CommitCheckOut(int reservationId)
    {
        Reservation? reservation = await con.reservations.FirstOrDefaultAsync(r => r.Id == reservationId);

        reservation.ReservationStatus = ReservationStatus.Completed;

        await con.SaveChangesAsync();
    }

    public async Task CommitCancelation(int reservationId)
    {
        Reservation? reservation = await con.reservations.FirstOrDefaultAsync(r => r.Id == reservationId);

        reservation.ReservationStatus = ReservationStatus.Cancelled;

        await con.SaveChangesAsync();
    }
}
