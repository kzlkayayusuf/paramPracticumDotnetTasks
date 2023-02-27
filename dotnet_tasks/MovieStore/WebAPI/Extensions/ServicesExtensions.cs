using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Presentation.Controllers;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebAPI.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
               services.AddDbContext<RepositoryContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerService, LoggerService>();

    public static void ConfigureActionFilters(this IServiceCollection services)
    {
        // ActionFilters IoC kaydı
        services.AddScoped<ValidationFilterAttribute>();
        services.AddSingleton<LogFilterAttribute>();
        services.AddScoped<ValidateMediaTypeAttribute>();
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
            );
        });
    }

    public static void ConfigureDataShaper(this IServiceCollection services) =>
        services.AddScoped<IDataShaper<MovieDto>, DataShaper<MovieDto>>();

    public static void AddCustomMediaTypes(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(config =>
        {
            var systemTextJsonOutputFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

            if (systemTextJsonOutputFormatter is not null)
            {
                systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.param.hateoas+json");

                systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.param.apiroot+json");
            }

            var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

            if (xmlOutputFormatter is not null)
            {
                xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.param.hateoas+xml");

                xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.param.apiroot+xml");
            }
        });
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");

            opt.Conventions.Controller<MoviesController>()
                .HasApiVersion(new ApiVersion(1, 0));

            opt.Conventions.Controller<MoviesV2Controller>()
                .HasDeprecatedApiVersion(new ApiVersion(2, 0));
        });
    }
}
