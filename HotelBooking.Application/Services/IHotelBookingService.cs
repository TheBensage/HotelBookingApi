using HotelBooking.Application.Commands;
using HotelBooking.Application.Queries;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Services;

public interface IHotelBookingService
{
    public Task<(IEnumerable<Hotel> Hotels, int TotalCount)> SearchHotelsAsync(HotelQuery query, CancellationToken cancellationToken = default);

    public Task<(IEnumerable<Room> Rooms, int TotalItems)> GetAvailableRoomsAsync(AvailabilityQuery query, CancellationToken cancellationToken = default);

    public Task<Booking?> CreateBooking(CreateBookingCommand command, CancellationToken cancellationToken = default);

    public Task<Booking?> GetBookingByReference(Guid reference, CancellationToken cancellationToken = default);
}
