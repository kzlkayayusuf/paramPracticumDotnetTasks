using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations;

public class TokenHandler
{
    public IConfiguration Configuration { get; set; }
    public TokenHandler(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public Token CreateAccessToken(User user)
    {
        Token tokenModel = new();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        tokenModel.Expiration = DateTime.Now.AddMinutes(15);
        JwtSecurityToken securityToken = new(
            issuer: Configuration["Token:Issuer"],
            audience: Configuration["Token:Audience"],
            expires: tokenModel.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler tokenHandler = new();
        // Creating token
        tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }

    private string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

}
