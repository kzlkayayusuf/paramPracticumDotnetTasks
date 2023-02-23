using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly RepositoryContext context;
    public MoviesController(RepositoryContext context)
    {
        this.context = context;

    }

    [HttpGet]
    public IActionResult GetAllMovies()
    {
        try
        {
            var movies = context.Movies.ToList();
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
            var movie = context.Movies.SingleOrDefault(m => m.Id.Equals(id));
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

            context.Movies.Add(movie);
            context.SaveChanges();

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
            var entity = context.Movies.SingleOrDefault(m => m.Id.Equals(id));

            if (entity is null)
                return NotFound();

            if (id != movie.Id)
                return BadRequest();

            entity.Name = movie.Name;
            entity.Genre = movie.Genre;
            entity.ReleaseYear = movie.ReleaseYear;
            entity.Price = movie.Price;
            context.SaveChanges();

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
            var entity = context.Movies.SingleOrDefault(m => m.Id.Equals(id));
            if (entity is null)
                return NotFound(new { statusCode = 404, message = $"Movie with id:{id} could not found" });

            context.Movies.Remove(entity);
            context.SaveChanges();

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
            var entity = context.Movies.SingleOrDefault(m => m.Id.Equals(id));

            if (entity is null)
                return NotFound();

            moviePatch.ApplyTo(entity);
            context.SaveChanges();

            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
