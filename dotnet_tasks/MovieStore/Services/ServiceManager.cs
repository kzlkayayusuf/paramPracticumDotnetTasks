using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    // lazy loading repositories katmanındaki gibi burada da uygulandı.
    private readonly Lazy<IMovieService> movieService;
    private readonly Lazy<IAuthenticationService> authenticationService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger,
        IMapper mapper, IMovieLinks movieLinks, UserManager<User> userManager, IConfiguration configuration)
    {
        this.movieService = new Lazy<IMovieService>(() => new MovieManager(repositoryManager, logger, mapper, movieLinks));
        this.authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationManager(logger, mapper, userManager, configuration));
    }
    public IMovieService MovieService => movieService.Value;

    public IAuthenticationService AuthenticationService => authenticationService.Value;
}
