using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto);
    Task<string> CreateToken();
}
