using Entities.Dtos;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;

namespace Services;

public class MovieLinks : IMovieLinks
{
    private readonly LinkGenerator linkGenerator;
    private readonly IDataShaper<MovieDto> dataShaper;

    public MovieLinks(LinkGenerator linkGenerator, IDataShaper<MovieDto> dataShaper)
    {
        this.linkGenerator = linkGenerator;
        this.dataShaper = dataShaper;
    }
    public LinkResponse TryGenerateLinks(IEnumerable<MovieDto> moviesDto, string fields, HttpContext httpContext)
    {
        var shapedMovies = ShapeData(moviesDto, fields);
        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkedMovies(moviesDto, fields, httpContext, shapedMovies);

        return ReturnShapedMovies(shapedMovies);
    }

    private LinkResponse ReturnLinkedMovies(IEnumerable<MovieDto> moviesDto, string fields, HttpContext httpContext, List<Entity> shapedMovies)
    {
        var movieDtoList = moviesDto.ToList();
        for (int index = 0; index < movieDtoList.Count(); index++)
        {
            var movieLinks = CreateLinkForMovie(httpContext, movieDtoList[index], fields);
            shapedMovies[index].Add("Links", movieLinks);
        }

        var movieCollection = new LinkCollectionWrapper<Entity>(shapedMovies);
        return new LinkResponse { HasLinks = true, LinkedEntities = movieCollection };
    }

    private List<Link> CreateLinkForMovie(HttpContext httpContext, MovieDto movieDto, string fields)
    {
        var links = new List<Link>()
        {
            new Link()
            {
                Href=$"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}"+
                    $"/{movieDto.Id}",

                Rel="self",
                Method="GET"
            },
            new Link()
            {
                Href=$"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",

                Rel="create",
                Method="POST"
            },
        };

        return links;
    }

    private LinkResponse ReturnShapedMovies(List<Entity> shapedMovies)
    {
        return new LinkResponse() { ShapedEntities = shapedMovies };
    }

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
        return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }

    private List<Entity> ShapeData(IEnumerable<MovieDto> moviesDto, string fields)
    {
        return dataShaper.ShapeData(moviesDto, fields).Select(m => m.Entity).ToList();
    }
}
