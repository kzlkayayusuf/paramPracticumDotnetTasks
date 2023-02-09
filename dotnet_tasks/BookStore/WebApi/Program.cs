global using WebApi.DBOperations;
global using WebApi.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase("BookStoreDb"));

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // for using automapper
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        //dependecy injection : AddSingleton,AddScoped, AddTransient
        builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
        builder.Services.AddSingleton<ILoggerService, DbLogger>();

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

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCustomExceptionMiddleware();

        app.MapControllers();

        app.Run();
    }
}