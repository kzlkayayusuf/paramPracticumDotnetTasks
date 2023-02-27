using System.Text.Json;
using Entities.Dtos;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

//[ApiVersion("1.0")]
[ServiceFilter(typeof(LogFilterAttribute))]
[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly IServiceManager manager;

    public MoviesController(IServiceManager manager)
    {
        this.manager = manager;
    }

    [HttpHead]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    [HttpGet(Name = "GetAllMovies")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAllMovies([FromQuery] MovieParameters movieParameters)
    {
        var linkParameters = new LinkParameters()
        {
            MovieParameters = movieParameters,
            HttpContext = HttpContext
        };

        var result = await manager.MovieService.GetAllMoviesAsync(linkParameters, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

        return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneMovie([FromRoute(Name = "id")] int id)
    {
        var movie = await manager.MovieService.GetOneMovieByIdAsync(id, false);

        return Ok(movie);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost(Name = "CreateOneMovie")]
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

    [HttpOptions]
    public IActionResult GetMoviesOptions()
    {
        Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, PATCH, HEAD, OPTIONS");
        return Ok();
    }
}