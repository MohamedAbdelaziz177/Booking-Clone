using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;

namespace BookingClone.Infrastructure.Persistance.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext con;
    public IHotelRepo HotelRepo { get; private set; }

    public IRoomRepo RoomRepo { get; private set; }

    public IRoomImageRepo RoomImageRepo { get; private set; }

    public IFeedbackRepo FeedbackRepo { get; private set; }

    public IReservationRepo ReservationRepo { get; private set; }

    public IUserRepo UserRepo { get; private set; }
    public IRefreshRokenRepo RefreshRokenRepo { get; private set; }

    public UnitOfWork(
        AppDbContext con,
        IHotelRepo hotelRepo,
        IRoomRepo roomRepo,
        IRoomImageRepo roomImageRepo,
        IFeedbackRepo feedbackRepo,
        IReservationRepo reservationRepo,
        IUserRepo userRepo,
        IRefreshRokenRepo refreshRokenRepo)
    {
        this.con = con;
        HotelRepo = hotelRepo;
        RoomRepo = roomRepo;
        RoomImageRepo = roomImageRepo;
        FeedbackRepo = feedbackRepo;
        ReservationRepo = reservationRepo;
        UserRepo = userRepo;
        RefreshRokenRepo = refreshRokenRepo;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await con.SaveChangesAsync();
    }
}
