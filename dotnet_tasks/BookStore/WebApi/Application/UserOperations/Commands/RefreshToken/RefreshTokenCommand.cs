using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken;

public class RefreshTokenCommand
{
    public string RefreshToken { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IConfiguration configuration;

    public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
    }

    public Token Handle()
    {
        var user = dbContext.Users.FirstOrDefault(u => u.RefreshToken == RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
        if (user is not null)
        {
            // Create Token
            TokenHandler tokenHandler = new(configuration);
            Token token = tokenHandler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            dbContext.SaveChanges();
            return token;
        }
        else
            throw new InvalidOperationException("No valid refresh token was found!");
    }
}
