﻿
namespace BookingClone.Application.Exceptions;

public class RegistrationFailedException : Exception
{
    public RegistrationFailedException(string message) : base(message) { }
}
