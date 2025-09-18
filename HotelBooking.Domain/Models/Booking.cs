namespace HotelBooking.Domain.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Guid Reference { get; set; }
    public Room Room { get; set; } = null!;
    public int RoomId { get; set; }
    public int Guests { get; set; }
}
