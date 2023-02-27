using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiVersion("2.0")]
[ApiController]
[Route("api/[controller]")]
public class MoviesV2Controller : ControllerBase
{
    private readonly IServiceManager manager;
    public MoviesV2Controller(IServiceManager manager)
    {
        this.manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMoviesAsync()
    {
        var movies = await manager.MovieService.GetAllMoviesAsync(false);
        return Ok(movies);
    }
}
