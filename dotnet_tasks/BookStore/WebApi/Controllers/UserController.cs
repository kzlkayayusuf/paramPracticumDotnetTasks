using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public UserController(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserCommand.CreateUserModel newUser)
    {
        CreateUserCommand command = new(dbContext, mapper);
        command.Model = newUser;
        command.Handle();

        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenCommand.CreateTokenModel newToken)
    {
        CreateTokenCommand command = new(dbContext, mapper, configuration);
        command.Model = newToken;
        var token = command.Handle();
        return token;
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new(dbContext, configuration);
        command.RefreshToken = token;
        var refreshedToken = command.Handle();
        return refreshedToken;
    }
}
