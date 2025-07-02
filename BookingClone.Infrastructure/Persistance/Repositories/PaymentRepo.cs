
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class PaymentRepo : GenericRepo<Payment>, IPaymentRepo
{
    public PaymentRepo(AppDbContext con) : base(con)
    {
    }
}
