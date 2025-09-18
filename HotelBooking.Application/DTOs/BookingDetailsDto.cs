using HotelBooking.Domain.Models;

namespace HotelBooking.Application.DTOs;

public record BookingDetailsDto
{
    public Guid Reference { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public static BookingDetailsDto Map(Booking booking)
    {
        return new BookingDetailsDto
        {
            Reference = booking.Reference,
            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate
        };
    }
}
