global using WebAPI.Models;
global using WebAPI.Common;
global using WebAPI.Services.CartoonService;
global using AutoMapper;
using WebAPI.Services.LogServices;
using WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Services.CartoonCharacterService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using WebAPI.Services.UserService;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<CartoonDbContext>(options => options.UseInMemoryDatabase("CartoonDb"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
*/

// for using automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//dependecy injection : AddSingleton,AddScoped, AddTransient
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
//builder.Services.AddSingleton<ILoggerService, DbLogger>();
builder.Services.AddScoped<ICartoonService, CartoonService>();
builder.Services.AddScoped<ICartoonCharacterService, CartoonCharacterService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartoonDbContext>(provider => provider.GetService<CartoonDbContext>());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

// use custom exception middleware for global log
app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
