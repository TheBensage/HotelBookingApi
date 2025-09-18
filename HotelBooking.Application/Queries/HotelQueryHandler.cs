using HotelBooking.Application.DTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Responses;
using HotelBooking.Application.Services;
using HotelBooking.Domain.Models;

namespace HotelBooking.Application.Queries;

public class HotelQueryHandler(IHotelBookingService hotelService) : IHandler<HotelQuery, Response<List<HotelSummaryDto>>>
{

    public async Task<Response<List<HotelSummaryDto>>> Handle(HotelQuery query, CancellationToken cancellationToken = default)
    {

        (IEnumerable<Hotel> hotels, int totalItems) = await hotelService.SearchHotelsAsync(query, cancellationToken);

        Pagination pagination = new()
        {
            Page = query.Page,
            PageSize = query.PageSize,
            TotalItems = totalItems
        };

        return Response<List<HotelSummaryDto>>.Ok(hotels.Select(HotelSummaryDto.Map).ToList(), pagination);
    }
}
