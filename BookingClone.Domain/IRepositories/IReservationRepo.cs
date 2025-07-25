

using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;
public interface IReservationRepo : IGenericRepo<Reservation>
{
    Task<List<Reservation>> GetByUserIdAsync(string userId);
    Task<List<Reservation>> GetByDateAsync(DateTime date,
        int pageIdx = 1,
        int pageSize = 3,
        string sortField = "Id",
        string sortType = "asc",
        int? hotelId = null);

    Task<List<Reservation>> GetExpiredReservations();

    Task CommitCheckIn(int reservationId);

    Task CommitCheckOut(int reservationId);

    Task CommitCancelation(int reservationId);


}
