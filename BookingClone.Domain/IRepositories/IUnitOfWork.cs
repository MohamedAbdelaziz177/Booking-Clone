﻿
using Microsoft.EntityFrameworkCore.Storage;

namespace BookingClone.Domain.IRepositories;

public interface IUnitOfWork
{
    IHotelRepo HotelRepo { get; }

    IRoomRepo RoomRepo { get; }

    IRoomImageRepo RoomImageRepo { get; }

    IFeedbackRepo FeedbackRepo { get; }

    IReservationRepo ReservationRepo { get; }

    IUserRepo UserRepo { get; }

    IRefreshRokenRepo RefreshRokenRepo { get; }

    IPaymentRepo PaymentRepo { get; }

    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> GetSerializableTransaction();

    Task<IDbContextTransaction> GetTransaction();

}
