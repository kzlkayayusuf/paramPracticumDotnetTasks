using Entities.Dtos;
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

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateOneMovie([FromBody] MovieDtoForInsertion movieDto)
    {
        if (movieDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); //422

        var movie = manager.MovieService.CreateOneMovie(movieDto);

        return StatusCode(201, movie);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateMovie([FromRoute(Name = "id")] int id, [FromBody] MovieDtoForUpdate movieDto)
    {
        if (movieDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); //422

        manager.MovieService.UpdateOneMovie(id, movieDto, false);

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
            [FromBody] JsonPatchDocument<MovieDtoForUpdate> moviePatch)
    {
        if (moviePatch is null)
            return BadRequest(); //400

        var result = manager.MovieService.GetOneMovieForPatch(id, false);

        moviePatch.ApplyTo(result.movieDtoForUpdate, ModelState);

        TryValidateModel(result.movieDtoForUpdate);
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); //422

        manager.MovieService.SaveChangesForPatch(result.movieDtoForUpdate, result.movie);

        return NoContent();
    }
}