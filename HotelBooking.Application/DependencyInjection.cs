using HotelBooking.Application.Commands;
using HotelBooking.Application.DTOs;
using HotelBooking.Application.Interfaces;
using HotelBooking.Application.Queries;
using HotelBooking.Application.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IHandler<HotelQuery, Response<List<HotelSummaryDto>>>, HotelQueryHandler>();
        services.AddScoped<IHandler<AvailabilityQuery, Response<List<RoomDto>>>, AvailabilityQueryHandler>();
        services.AddScoped<IHandler<BookingQuery, Response<BookingDetailsDto>>, BookingQueryHandler>();
        services.AddScoped<IHandler<CreateBookingCommand, Response<BookingDetailsDto>>, CreateBookingCommandHandler>();
        return services;
    }
}
