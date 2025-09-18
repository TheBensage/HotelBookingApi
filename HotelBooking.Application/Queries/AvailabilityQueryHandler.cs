using HotelBooking.Application.DTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Responses;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Queries;

public class AvailabilityQueryHandler(IHotelBookingService hotelBookingService) : IHandler<AvailabilityQuery, Response<List<RoomDto>>>
{
    public async Task<Response<List<RoomDto>>> Handle(AvailabilityQuery query, CancellationToken cancellationToken = default)
    {
        if (query.CheckIn >= query.CheckOut)
        {
            return Response<List<RoomDto>>.Fail("Invalid date range.");
        }

        if (query.Guests <= 0)
        {
            return Response<List<RoomDto>>.Fail("Guests must be greater than zero.");
        }

        (IEnumerable<Room> rooms, int totalItems) =
            await hotelBookingService.GetAvailableRoomsAsync(query, cancellationToken);

        Pagination pagination = new()
        {
            Page = query.Page,
            PageSize = query.PageSize,
            TotalItems = totalItems
        };

        return Response<List<RoomDto>>.Ok(rooms.Select(RoomDto.Map).ToList(), pagination);
    }
}
