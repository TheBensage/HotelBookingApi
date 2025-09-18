using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Models;

public class Room
{
    public int Id { get; set; }

    public EnRoomType Type { get; set; }
    public int Capacity { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public int RoomNumber { get; set; }
    public int HotelId { get; set; }

    public List<Booking> Bookings { get; set; } = new();
}
