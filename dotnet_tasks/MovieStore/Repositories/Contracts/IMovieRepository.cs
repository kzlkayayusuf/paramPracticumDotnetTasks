using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts;

public interface IMovieRepository : IRepositoryBase<Movie>
{
    Task<PagedList<Movie>> GetAllMoviesAsync(MovieParameters movieParameters, bool trackChanges);
    Task<Movie> GetOneMovieByIdAsync(int id, bool trackChanges);
    void CreateOneMovie(Movie movie);
    void UpdateOneMovie(Movie movie);
    void DeleteOneMovie(Movie movie);
}
