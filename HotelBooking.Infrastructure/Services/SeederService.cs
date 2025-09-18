using HotelBooking.Application.Services;
using HotelBooking.Domain.Enums;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Services;

public class SeederService(AppDbContext dbContext) : ISeederService
{
    private static readonly string[] HotelNames =
        { "Grand Plaza", "Ocean View", "City Lights", "Mountain Retreat", "Sunset Inn", "Riverside Hotel", "Lakeside Lodge" };

    private static readonly Random Random = new();

    public async Task SeedAsync()
    {
        if (await dbContext.Hotels.AnyAsync())
            return;

        List<Hotel> seedHotels = CreateHotels(HotelNames.ToList());

        dbContext.Hotels.AddRange(seedHotels);
        await dbContext.SaveChangesAsync();
    }

    public async Task ResetAsync()
    {
        dbContext.Hotels.RemoveRange(dbContext.Hotels);
        await dbContext.SaveChangesAsync();
    }

    public List<Hotel> CreateHotels(List<string> hotelNames)
    {
        List<Hotel> hotels = new();

        foreach (string hotelName in hotelNames)
        {
            Hotel hotel = new Hotel
            {
                Name = hotelName,
                Rooms = new()
            };

            for (int i = 0; i < 6; i++)
            {
                EnRoomType roomType = RandomRoomType();
                hotel.Rooms.Add(new Room
                {
                    RoomNumber = i + 1,
                    Type = roomType,
                    Capacity = GetCapacityForRoomType(roomType),
                    Hotel = hotel
                });
            }

            hotels.Add(hotel);
        }

        return hotels;
    }

    private static EnRoomType RandomRoomType()
    {
        Array values = Enum.GetValues(typeof(EnRoomType));
        return (EnRoomType)values.GetValue(Random.Next(values.Length))!;
    }

    private static int GetCapacityForRoomType(EnRoomType type)
    {
        return type switch
        {
            EnRoomType.Single => 1,
            EnRoomType.Double => 2,
            EnRoomType.Deluxe => 3,
            _ => 1
        };
    }
}
