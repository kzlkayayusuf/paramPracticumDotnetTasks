using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebAPI.Controllers;

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
        try
        {
            var movies = manager.MovieService.GetAllMovies(false);
            return Ok(movies);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOneMovie([FromRoute(Name = "id")] int id)
    {
        try
        {
            var movie = manager.MovieService.GetOneMovieById(id, false);
            if (movie is null)
                return NotFound();

            return Ok(movie);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateOneMovie([FromBody] Movie movie)
    {
        try
        {
            if (movie is null)
                return BadRequest();

            manager.MovieService.CreateOneMovie(movie);

            return StatusCode(201, movie);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateMovie([FromRoute(Name = "id")] int id, [FromBody] Movie movie)
    {
        try
        {
            if (movie is null)
                return BadRequest();

            manager.MovieService.UpdateOneMovie(id, movie, true);

            return NoContent(); //204
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOneMovie([FromRoute(Name = "id")] int id)
    {
        try
        {
            manager.MovieService.DeleteOneMovie(id, false);

            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public IActionResult PartiallyUpdateOneMovie([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Movie> moviePatch)
    {
        try
        {
            var entity = manager.MovieService.GetOneMovieById(id, true);

            if (entity is null)
                return NotFound();

            moviePatch.ApplyTo(entity);
            manager.MovieService.UpdateOneMovie(id, entity, true);

            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
