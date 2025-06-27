using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class UserRepo : GenericRepo<User>, IUserRepo
{
    public UserRepo(AppDbContext con) : base(con)
    {
    }
}
