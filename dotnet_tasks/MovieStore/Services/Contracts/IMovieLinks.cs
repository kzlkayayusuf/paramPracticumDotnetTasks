using Entities.Dtos;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts;

public interface IMovieLinks
{
    LinkResponse TryGenerateLinks(IEnumerable<MovieDto> moviesDto, string fields, HttpContext httpContext);
}
