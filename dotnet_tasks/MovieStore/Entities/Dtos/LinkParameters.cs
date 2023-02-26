using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos;

public record LinkParameters
{
    public MovieParameters MovieParameters { get; init; }
    public HttpContext HttpContext { get; init; }
}
