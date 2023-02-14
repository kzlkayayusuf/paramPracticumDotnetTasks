using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.CartoonOperations.Commands.CreateCartoon;
using WebAPI.Application.CartoonOperations.Commands.UpdateCartoon;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/v1.0/[controller]")]
public class CartoonController : ControllerBase
{
    private readonly ICartoonService cartoonService;

    public CartoonController(ICartoonService cartoonService)
    {
        this.cartoonService = cartoonService;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var response = cartoonService.GetAllCartoons();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult GetSingle(int id)
    {
        var response = cartoonService.GetCartoonById(id);

        return Ok(response);
    }

    [HttpGet]
    [Route("ListByName")]
    public ActionResult GetByName([FromQuery] string name)
    {
        var response = cartoonService.GetCartoonByName(name);

        return Ok(response);
    }

    [HttpGet]
    [Route("ListByGenre")]
    public ActionResult GetByGenre([FromQuery] Genre genre)
    {
        var response = cartoonService.GetCartoonsByGenre(genre);

        return Ok(response);
    }

    [HttpGet]
    [Route("ListByCharacters")]
    public ActionResult GetByCharacters([FromQuery] List<string> characters)
    {
        var response = cartoonService.GetCartoonByCharacters(characters);

        return Ok(response);
    }

    [HttpPost]
    public ActionResult AddCartoon([FromBody] CreateCartoonCommand.CreateCartoonModel newCartoon)
    {
        var response = cartoonService.AddCartoon(newCartoon);

        return Created(Url.Action("GetSingle", new { id = response.Data.ID }), response);
        //Headers Location kısmında bu uriyi gösterir.
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCartoon(int id, [FromBody] UpdateCartoonCommand.UpdateCartoonModel updatedCartoon)
    {
        var response = cartoonService.UpdateCartoon(id, updatedCartoon);

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateCartoonPatch(int id, [FromBody] UpdateCartoonCommand.UpdateCartoonModel updatedCartoon)
    {
        var response = cartoonService.UpdateCartoon(id, updatedCartoon);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCartoon(int id)
    {
        var response = cartoonService.DeleteCartoon(id);

        return Ok(response);
    }
}
