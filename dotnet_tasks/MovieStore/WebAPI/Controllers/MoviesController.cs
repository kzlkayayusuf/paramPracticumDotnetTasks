using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IRepositoryManager manager;

    public MoviesController(IRepositoryManager manager)
    {
        this.manager = manager;
    }

    [HttpGet]
    public IActionResult GetAllMovies()
    {
        try
        {
            var movies = manager.Movie.GetAllMovies(false);
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
            var movie = manager.Movie.GetOneMovieById(id, false);
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

            manager.Movie.CreateOneMovie(movie);
            manager.Save();

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
            var entity = manager.Movie.GetOneMovieById(id, true);

            if (entity is null)
                return NotFound();

            if (id != movie.Id)
                return BadRequest();

            entity.Name = movie.Name;
            entity.Genre = movie.Genre;
            entity.ReleaseYear = movie.ReleaseYear;
            entity.Price = movie.Price;
            manager.Save();

            return Ok(movie);
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
            var entity = manager.Movie.GetOneMovieById(id, false);
            if (entity is null)
                return NotFound(new { statusCode = 404, message = $"Movie with id:{id} could not found" });

            manager.Movie.DeleteOneMovie(entity);
            manager.Save();

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
            var entity = manager.Movie.GetOneMovieById(id, true);

            if (entity is null)
                return NotFound();

            moviePatch.ApplyTo(entity);
            manager.Movie.Update(entity);
            manager.Save();

            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
