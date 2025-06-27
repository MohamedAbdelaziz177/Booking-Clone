using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
using System;

namespace BookingClone.Application.Features.Room.Responses;

public class RoomResponseDto
{
    public int Id { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public decimal PricePerNight { get; set; }

    public string Type { get; set; } = default!;

    public bool isAvailable { get; set; } = true;

    public int HotelId { get; set; }

    public string HotelName { get; set; } = default!;
    public List<RoomImgDto> RoomImageDtos { get; set; } = new List<RoomImgDto>();
    //
    //public List<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
}
