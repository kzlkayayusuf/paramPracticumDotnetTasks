using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    // lazy loading repositories katmanındaki gibi burada da uygulandı.
    private readonly Lazy<IMovieService> movieService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger)
    {
        this.movieService = new Lazy<IMovieService>(() => new MovieManager(repositoryManager, logger));
    }
    public IMovieService MovieService => movieService.Value;
}
