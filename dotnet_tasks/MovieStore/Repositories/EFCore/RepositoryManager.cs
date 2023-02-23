using Repositories.Contracts;

namespace Repositories.EFCore;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext context;
    // lazy loading -> nesne ancak ve ancak istenildiği an newlenip kullanılacak
    private readonly Lazy<IMovieRepository> movieRepository;

    public RepositoryManager(RepositoryContext context)
    {
        this.context = context;
        this.movieRepository = new Lazy<IMovieRepository>(() => new MovieRepository(context));
    }

    // container a injecte etmemek için burada movierepository newlendi.
    // public IMovieRepository Movie => new MovieRepository(context);
    // yukarıdaki gibi her seferinde newlemek yerine lazy ile nesne istenildiğinde newlenir.
    public IMovieRepository Movie => movieRepository.Value;
    public void Save()
    {
        context.SaveChanges();
    }
}
