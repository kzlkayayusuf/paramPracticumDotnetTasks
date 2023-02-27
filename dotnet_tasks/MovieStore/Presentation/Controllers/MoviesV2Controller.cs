using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiVersion("2.0", Deprecated = true)]
[ApiController]
[Route("api/movies")]
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
        var moviesV2 = movies.Select(m => new
        {
            Name = m.Name,
            Id = m.Id
        });
        return Ok(moviesV2);
    }
}
