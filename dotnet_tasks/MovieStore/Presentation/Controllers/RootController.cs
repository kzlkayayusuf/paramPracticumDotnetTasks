using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers;

[ApiController]
[Route("api")]
[ApiExplorerSettings(GroupName = "v1")]
public class RootController : ControllerBase
{
    private readonly LinkGenerator linkGenerator;

    public RootController(LinkGenerator linkGenerator)
    {
        this.linkGenerator = linkGenerator;
    }

    [HttpGet(Name = "GetRoot")]
    public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")] string mediaType)
    {
        if (mediaType.Contains("application/vnd.param.apiroot"))
        {
            var list = new List<Link>
            {
                new Link()
                {
                    Href=linkGenerator.GetUriByName(HttpContext,nameof(GetRoot),new{}),
                    Rel="self",
                    Method="GET"
                },
                new Link()
                {
                    Href=linkGenerator.GetUriByName(HttpContext,nameof(MoviesController.GetAllMovies),new{}),
                    Rel="movies",
                    Method="GET"
                },
                 new Link()
                {
                    Href=linkGenerator.GetUriByName(HttpContext,nameof(MoviesController.CreateOneMovie),new{}),
                    Rel="movies",
                    Method="POST"
                }
            };

            return Ok(list);
        }
        return NoContent(); //204
    }
}
