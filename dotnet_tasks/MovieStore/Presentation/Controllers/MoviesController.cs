using System.Text.Json;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

[ServiceFilter(typeof(LogFilterAttribute))]
[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IServiceManager manager;

    public MoviesController(IServiceManager manager)
    {
        this.manager = manager;
    }

    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [HttpGet]
    public async Task<IActionResult> GetAllMovies([FromQuery] MovieParameters movieParameters)
    {
        var pagedResult = await manager.MovieService.GetAllMoviesAsync(movieParameters, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.movies);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneMovie([FromRoute(Name = "id")] int id)
    {
        var movie = await manager.MovieService.GetOneMovieByIdAsync(id, false);

        return Ok(movie);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateOneMovie([FromBody] MovieDtoForInsertion movieDto)
    {
        var movie = await manager.MovieService.CreateOneMovieAsync(movieDto);

        return StatusCode(201, movie);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMovie([FromRoute(Name = "id")] int id, [FromBody] MovieDtoForUpdate movieDto)
    {
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