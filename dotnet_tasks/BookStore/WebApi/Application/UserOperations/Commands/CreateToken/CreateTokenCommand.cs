using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken;

public class CreateTokenCommand
{
    public CreateTokenModel Model { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public Token Handle()
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
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
            throw new InvalidOperationException("Wrong username and password!");
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
