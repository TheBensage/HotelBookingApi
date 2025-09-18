using HotelBooking.Application.DTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Responses;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Commands;

public class CreateBookingCommandHandler(IHotelBookingService hotelBookingService) : IHandler<CreateBookingCommand, Response<BookingDetailsDto>>
{
    public async Task<Response<BookingDetailsDto>> Handle(CreateBookingCommand command, CancellationToken cancellationToken = default)
    {

        if (command.CheckInDate.Date < DateTime.Now.Date)
        {
            return Response<BookingDetailsDto>.Fail("You can't book check in yesterday");
        }

        if (command.CheckOutDate.Date < command.CheckInDate.Date)
        {
            return Response<BookingDetailsDto>.Fail("You can't book check out before you check in");
        }

        Booking? booking = await hotelBookingService.CreateBooking(command, cancellationToken);

        if (booking == null)
        {
            return Response<BookingDetailsDto>.Fail("Room is not available or booking could not be created.");
        }

        return Response<BookingDetailsDto>.Ok(BookingDetailsDto.Map(booking));
    }
}
