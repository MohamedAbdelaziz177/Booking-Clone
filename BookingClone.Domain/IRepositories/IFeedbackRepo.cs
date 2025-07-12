
using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;

public interface IFeedbackRepo : IGenericRepo<FeedBack>
{
    Task<List<FeedBack>> GetFeedBacksByRoomIdAsync(int roomId);

    Task<List<FeedBack>> GetFeedBacksByReservationIdAsync(int reservationId);
}
