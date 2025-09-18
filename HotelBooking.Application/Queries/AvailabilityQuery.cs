using System.ComponentModel.DataAnnotations;
using HotelBooking.Domain.Enums;

namespace HotelBooking.Application.Queries;

public class AvailabilityQuery : PagedQuery
{
    public int? HotelId { get; set; }

    [DataType(DataType.Date)]
    public DateOnly CheckIn { get; set; }

    [DataType(DataType.Date)]
    public DateOnly CheckOut { get; set; }
    public int Guests { get; set; }
    public EnRoomType? RoomType { get; set; }
}
