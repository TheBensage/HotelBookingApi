using HotelBooking.Application.DTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Responses;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Queries;

public class BookingQueryHandler(IHotelBookingService hotelBookingService) : IHandler<BookingQuery, Response<BookingDetailsDto>>
{
    public async Task<Response<BookingDetailsDto>> Handle(BookingQuery query, CancellationToken cancellationToken = default)
    {
        Booking? booking = await hotelBookingService.GetBookingByReference(query.Reference);

        if (booking == null)
        {
            return Response<BookingDetailsDto>.Fail("No booking");
        }

        return Response<BookingDetailsDto>.Ok(BookingDetailsDto.Map(booking));
    }
}
