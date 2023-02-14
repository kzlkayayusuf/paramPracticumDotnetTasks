using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebAPI.DBOperations;
using WebAPI.TokenOperations;
using WebAPI.TokenOperations.Models;

namespace WebAPI.Application.UserOperations.Commands.CreateToken;

public class CreateTokenCommand
{
    public CreateTokenModel Model { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public CreateTokenCommand(ICartoonDbContext context, IMapper mapper, IConfiguration configuration)
    {
        this.context = context;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public ServiceResponse<Token> Handle()
    {
        var user = context.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
        if (user is not null)
        {
            // Create Token
            TokenHandler tokenHandler = new(configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            context.SaveChanges();
            return new ServiceResponse<Token>(token);
        }
        else
            throw new InvalidOperationException("Wrong username or password!");
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
