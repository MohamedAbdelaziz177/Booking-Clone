﻿
namespace BookingClone.Application.Common;

public static class MagicValues
{
    public static readonly int MIN_OTP_VAL = 10000;
    public static readonly int MAX_OTP_VAL = 100000;
    public static readonly int OTP_LIFE_TIME_MINS = 5;

    public static readonly int REFRESH_TOKEN_LIFE_TIME_DAYS = 15;
    public static readonly int ACCESS_TOKEN_LIFE_TIME_MINS = 15;

    public static readonly int OTP_EXPIRY_MINS = 5;

    public static readonly int PAGE_SIZE = 3;

    public static readonly string HOTEL_REDIS_KEY = "HOTEL";
    public static readonly string ROOM_REDIS_KEY = "ROOM";

    public static readonly string HOTEL_PAGE_REDIS_TAG = "H_TAG";
    public static readonly string ROOM_PAGE_REDIS_TAG = "R_TAG";

}
