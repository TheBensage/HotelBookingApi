﻿namespace HotelBooking.Application.Commands;

public class CreateBookingCommand
{
    public int RoomId { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public int Guests { get; set; }
}
