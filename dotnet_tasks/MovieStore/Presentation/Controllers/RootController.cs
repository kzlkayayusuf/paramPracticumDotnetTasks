using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers;

[ApiController]
[Route("api")]
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

        }
        return NoContent(); //204
    }
}
