using HotelBooking.Application.Commands;
using HotelBooking.Application.Queries;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Models;
using HotelBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Services;

public class HotelBookingService(AppDbContext dbContext) : IHotelBookingService
{

    public async Task<(IEnumerable<Hotel> Hotels, int TotalCount)> SearchHotelsAsync(HotelQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<Hotel> queryable = dbContext.Hotels.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            queryable = queryable.Where(h => h.Name.Contains(query.Name));
        }

        int total = await queryable.CountAsync(cancellationToken);

        List<Hotel> hotels = await queryable
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return (hotels, total);
    }

    public async Task<(IEnumerable<Room> Rooms, int TotalItems)> GetAvailableRoomsAsync(AvailabilityQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<Room> roomsQuery = dbContext.Rooms
            .Include(r => r.Bookings)
            .Where(r => r.Capacity >= query.Guests);

        if (query.RoomType != null)
        {
            roomsQuery = roomsQuery.Where(r => r.Type == query.RoomType);
        }

        if (query.HotelId != null)
        {
            roomsQuery = roomsQuery.Where(r => r.HotelId == query.HotelId);
        }

        DateTime checkIn = query.CheckIn.ToDateTime(TimeOnly.MinValue);
        DateTime checkOut = query.CheckOut.ToDateTime(TimeOnly.MinValue);

        roomsQuery = roomsQuery.Where(r => r.Bookings
            .All(b => b.CheckOutDate <= checkIn || b.CheckInDate >= checkOut));

        int totalItems = await roomsQuery.CountAsync(cancellationToken);

        List<Room> rooms = await roomsQuery
            .OrderBy(r => r.Type)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return (rooms, totalItems);
    }

    public async Task<Booking?> CreateBooking(
        CreateBookingCommand command,
        CancellationToken cancellationToken = default)
    {
        Room? room = await dbContext.Rooms
            .Include(r => r.Bookings)
            .FirstOrDefaultAsync(r => r.Id == command.RoomId, cancellationToken);

        if (room == null)
        {
            return null;
        }

        bool isAvailable = !await dbContext.Bookings
            .AnyAsync(b =>
                b.RoomId == room.Id &&
                command.CheckInDate.Date < b.CheckOutDate.Date &&
                command.CheckOutDate.Date > b.CheckInDate.Date,
                cancellationToken);

        if (!isAvailable || command.Guests > room.Capacity)
        {
            return null;
        }

        Booking booking = new()
        {
            Reference = Guid.NewGuid(),
            RoomId = room.Id,
            CheckInDate = command.CheckInDate.Date,
            CheckOutDate = command.CheckOutDate.Date,
            Guests = command.Guests,
        };

        dbContext.Bookings.Add(booking);
        await dbContext.SaveChangesAsync(cancellationToken);

        return booking;
    }

    public async Task<Booking?> GetBookingByReference(
        Guid reference,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .FirstOrDefaultAsync(b => b.Reference == reference, cancellationToken);
    }
}
