using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services;

public class AuthenticationManager : IAuthenticationService
{
    private readonly ILoggerService logger;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;

    public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.userManager = userManager;
        this.configuration = configuration;
    }
    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
    {
        var user = mapper.Map<User>(userForRegistrationDto);

        var result = await userManager.CreateAsync(user, userForRegistrationDto.Password);

        if (result.Succeeded)
            await userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);

        return result;
    }
}
