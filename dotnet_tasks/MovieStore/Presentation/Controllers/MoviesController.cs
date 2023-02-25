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
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await manager.MovieService.GetAllMoviesAsync(false);
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneMovie([FromRoute(Name = "id")] int id)
    {
        var movie = await manager.MovieService.GetOneMovieByIdAsync(id, false);

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOneMovie([FromBody] MovieDtoForInsertion movieDto)
    {
        if (movieDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); //422

        var movie = await manager.MovieService.CreateOneMovieAsync(movieDto);

        return StatusCode(201, movie);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMovie([FromRoute(Name = "id")] int id, [FromBody] MovieDtoForUpdate movieDto)
    {
        if (movieDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); //422

        await manager.MovieService.UpdateOneMovieAsync(id, movieDto, false);

        return NoContent(); //204
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneMovie([FromRoute(Name = "id")] int id)
    {
        await manager.MovieService.DeleteOneMovieAsync(id, false);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PartiallyUpdateOneMovie([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<MovieDtoForUpdate> moviePatch)
    {
        if (moviePatch is null)
            return BadRequest(); //400

        var result = await manager.MovieService.GetOneMovieForPatchAsync(id, false);

        moviePatch.ApplyTo(result.movieDtoForUpdate, ModelState);

        TryValidateModel(result.movieDtoForUpdate);
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState); //422

        await manager.MovieService.SaveChangesForPatchAsync(result.movieDtoForUpdate, result.movie);

        return NoContent();
    }
}