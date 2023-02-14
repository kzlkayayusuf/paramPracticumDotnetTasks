using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.UserOperations.Commands.CreateToken;
using WebAPI.Application.UserOperations.Commands.CreateUser;
using WebAPI.Application.UserOperations.Commands.UpdateUser;
using WebAPI.Services.UserService;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService service;

    public UserController(IUserService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var response = service.GetAllUsers();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult GetSingle(int id)
    {
        var response = service.GetUserById(id);

        return Ok(response);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateUserCommand.CreateUserModel newUser)
    {
        var response = service.CreateUser(newUser);

        return Ok(response);
    }

    [HttpPost("connect/token")]
    public ActionResult CreateToken([FromBody] CreateTokenCommand.CreateTokenModel newToken)
    {
        var response = service.CreateToken(newToken);

        return Ok(response);
    }

    [HttpGet("refreshToken")]
    public ActionResult RefreshToken([FromQuery] string token)
    {
        var response = service.RefreshToken(token);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var response = service.DeleteUser(id);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCartoon(int id, [FromBody] UpdateUserCommand.UpdateUserModel updatedUser)
    {
        var response = service.UpdateUser(id, updatedUser);

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateUserPatch(int id, [FromBody] UpdateUserCommand.UpdateUserModel updatedUser)
    {
        var response = service.UpdateUser(id, updatedUser);

        return Ok(response);
    }
}
