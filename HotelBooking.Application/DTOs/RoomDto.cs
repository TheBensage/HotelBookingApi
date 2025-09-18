using HotelBooking.Domain.Enums;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.DTOs;

public record RoomDto
{
    public int Id { get; set; }
    public EnRoomType Type { get; set; }
    public int Capacity { get; set; }
    public int RoomNumber { get; set; }
    public int HotelId { get; set; }

    public static RoomDto Map(Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Type = room.Type,
            Capacity = room.Capacity,
            RoomNumber = room.RoomNumber,
            HotelId = room.HotelId
        };
    }
}
