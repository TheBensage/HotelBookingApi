using HotelBooking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/seed")]
public class SeedController : ControllerBase
{
    [HttpPost("seed")]
    public async Task<IActionResult> Seed([FromServices] ISeederService seeder)
    {
        await seeder.SeedAsync();
        return Ok("Seeded database");
    }

    [HttpPost("reset")]
    public async Task<IActionResult> Reset([FromServices] ISeederService seeder)
    {
        await seeder.ResetAsync();
        return Ok("Reset done");
    }
}
