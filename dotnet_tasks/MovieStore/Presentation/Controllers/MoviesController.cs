using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IServiceManager manager;

    public MoviesController(IServiceManager manager)
    {
        this.manager = manager;
    }

    [HttpGet]
    public IActionResult GetAllMovies()
    {
        var movies = manager.MovieService.GetAllMovies(false);
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOneMovie([FromRoute(Name = "id")] int id)
    {
        var movie = manager.MovieService.GetOneMovieById(id, false);
        if (movie is null)
            return NotFound();

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateOneMovie([FromBody] Movie movie)
    {
        if (movie is null)
            return BadRequest();

        manager.MovieService.CreateOneMovie(movie);

        return StatusCode(201, movie);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateMovie([FromRoute(Name = "id")] int id, [FromBody] Movie movie)
    {
        if (movie is null)
            return BadRequest();

        manager.MovieService.UpdateOneMovie(id, movie, true);

        return NoContent(); //204
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOneMovie([FromRoute(Name = "id")] int id)
    {
        manager.MovieService.DeleteOneMovie(id, false);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public IActionResult PartiallyUpdateOneMovie([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Movie> moviePatch)
    {
        var entity = manager.MovieService.GetOneMovieById(id, true);

        if (entity is null)
            return NotFound();

        moviePatch.ApplyTo(entity);
        manager.MovieService.UpdateOneMovie(id, entity, true);

        return NoContent();
    }
}