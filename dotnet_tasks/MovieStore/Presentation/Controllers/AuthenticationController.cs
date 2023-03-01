using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/auth")]
[ApiExplorerSettings(GroupName = "v1")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager manager;

    public AuthenticationController(IServiceManager manager)
    {
        this.manager = manager;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
        var result = await manager.AuthenticationService.RegisterUser(userForRegistrationDto);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthDto)
    {
        if (!await manager.AuthenticationService.ValidateUser(userForAuthDto))
            return Unauthorized(); //401

        var tokenDto = await manager.AuthenticationService.CreateToken(true);

        return Ok(tokenDto);
    }

    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoForReturn = await manager.AuthenticationService.RefreshToken(tokenDto);

        return Ok(tokenDtoForReturn);
    }
}
