using HotelBooking.Domain.Models;

namespace HotelBooking.Application.DTOs;

public class HotelSummaryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public static HotelSummaryDto Map(Hotel hotel)
    {
        return new HotelSummaryDto
        {
            Id = hotel.Id,
            Name = hotel.Name,
        };
    }
}
