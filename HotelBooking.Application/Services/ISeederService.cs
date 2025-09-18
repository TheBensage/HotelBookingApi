namespace HotelBooking.Application.Services;

public interface ISeederService
{
    Task ResetAsync();
    Task SeedAsync();
}
