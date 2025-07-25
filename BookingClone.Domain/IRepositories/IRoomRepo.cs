﻿
using BookingClone.Domain.Entities;

namespace BookingClone.Domain.IRepositories;
public interface IRoomRepo : IGenericRepo<Room>
{
    Task<List<Room>> GetByHotelId(int hotelId);
    Task<bool> CheckAvailableBetweenAsync(int roomId, DateTime start, DateTime end);
    Task<List<Room>> GetAvaliableRoomsBetween(DateTime start,
        DateTime end,
        int? hotelId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int pageIdx = 1,
        int pageSize = 3,
        string sortField = "Id",
        string sortDir = "desc");
}
