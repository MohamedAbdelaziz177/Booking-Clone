using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class FeedbackRepo : GenericRepo<FeedBack>, IFeedbackRepo
{
    public FeedbackRepo(AppDbContext con) : base(con)
    {
    }
}
