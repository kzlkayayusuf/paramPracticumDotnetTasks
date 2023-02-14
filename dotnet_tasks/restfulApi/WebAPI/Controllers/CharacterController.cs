using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.CharacterOperations.Commands.CreateCharacter;
using WebAPI.Application.CharacterOperations.Commands.UpdateCharacter;
using WebAPI.Services.CartoonCharacterService;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/v1.0/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICartoonCharacterService service;

    public CharacterController(ICartoonCharacterService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var response = service.GetAllCharacters();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult GetSingle(int id)
    {
        var response = service.GetCharacterById(id);

        return Ok(response);
    }

    [HttpGet]
    [Route("ListByName")]
    public ActionResult GetByName([FromQuery] string name)
    {
        var response = service.GetCharacterByName(name);

        return Ok(response);
    }

    [HttpPost]
    public ActionResult AddCharacter([FromBody] CreateCharacterCommand.CreateCharacterModel newCharacter)
    {
        var response = service.AddCharacter(newCharacter);

        return Created(Url.Action("GetSingle", new { id = response.Data.ID }), response);
        //Headers Location kısmında bu uriyi gösterir.
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCharacter(int id, [FromBody] UpdateCharacterCommand.UpdateCharacterModel updatedCharacter)
    {
        var response = service.UpdateCharacter(id, updatedCharacter);

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateCharacterPatch(int id, [FromBody] UpdateCharacterCommand.UpdateCharacterModel updatedCharacter)
    {
        var response = service.UpdateCharacter(id, updatedCharacter);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCharacter(int id)
    {
        var response = service.DeleteCharacter(id);

        return Ok(response);
    }
}
