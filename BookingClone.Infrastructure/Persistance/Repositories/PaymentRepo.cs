﻿
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class PaymentRepo : GenericRepo<Payment>, IPaymentRepo
{
    public PaymentRepo(AppDbContext con) : base(con)
    {
    }

    public async Task<Payment?> GetPaymentByReservatioIdAsync(int reservationId)
    {
        return await con.payments
            .Include(p => p.Reservation)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
    }
}
