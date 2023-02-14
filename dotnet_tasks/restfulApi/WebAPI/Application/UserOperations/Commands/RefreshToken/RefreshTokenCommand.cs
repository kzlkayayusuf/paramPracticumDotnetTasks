using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebAPI.DBOperations;
using WebAPI.TokenOperations;
using WebAPI.TokenOperations.Models;

namespace WebAPI.Application.UserOperations.Commands.RefreshToken;

public class RefreshTokenCommand
{
    public string RefreshToken { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IConfiguration configuration;

    public RefreshTokenCommand(ICartoonDbContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public ServiceResponse<Token> Handle()
    {
        var user = context.Users.FirstOrDefault(u => u.RefreshToken == RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
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
            throw new InvalidOperationException("No valid refresh token was found!");
    }
}

