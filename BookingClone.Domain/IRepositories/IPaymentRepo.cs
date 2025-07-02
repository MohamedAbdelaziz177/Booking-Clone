
using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;

public interface IPaymentRepo : IGenericRepo<Payment>
{
    Task<Payment?> GetPaymentByReservatioIdAsync(int reservationId);
}
