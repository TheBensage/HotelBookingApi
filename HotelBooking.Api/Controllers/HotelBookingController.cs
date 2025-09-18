using HotelBooking.Application.Commands;
using HotelBooking.Application.DTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Queries;
using HotelBooking.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/")]
public class HotelBookingController : ControllerBase
{
    [HttpGet("search")]
    public async Task<IActionResult> SearchHotels(
        [FromQuery] HotelQuery query,
        [FromServices] IHandler<HotelQuery, Response<List<HotelSummaryDto>?>> handler)
    {
        Response<List<HotelSummaryDto>?> result = await handler.Handle(query);
        return Ok(result);
    }

    [HttpGet("availability")]

    public async Task<IActionResult> SearchAvailability(
        [FromQuery] AvailabilityQuery query,
        [FromServices] IHandler<AvailabilityQuery, Response<List<RoomDto>?>> handler)
    {
        Response<List<RoomDto>?> result = await handler.Handle(query);
        return Ok(result);
    }


    [HttpGet("booking")]
    public async Task<IActionResult> GetBooking(
        [FromQuery] BookingQuery query,
        [FromServices] IHandler<BookingQuery, Response<BookingDetailsDto>?> handler)
    {
        Response<BookingDetailsDto>? result = await handler.Handle(query);
        return Ok(result);
    }

    [HttpPost("booking")]
    public async Task<IActionResult> CreateBookng(
    [FromBody] CreateBookingCommand command,
    [FromServices] IHandler<CreateBookingCommand, Response<BookingDetailsDto>?> handler)
    {
        Response<BookingDetailsDto>? result = await handler.Handle(command);
        return Ok(result);
    }
}
