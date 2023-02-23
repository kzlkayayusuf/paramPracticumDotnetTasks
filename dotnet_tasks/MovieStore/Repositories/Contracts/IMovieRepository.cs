using Entities.Models;

namespace Repositories.Contracts;

public interface IMovieRepository : IRepositoryBase<Movie>
{
    IQueryable<Movie> GetAllMovies(bool trackChanges);
    Movie GetOneMovieById(int id, bool trackChanges);
    void CreateOneMovie(Movie movie);
    void UpdateOneMovie(Movie movie);
    void DeleteOneMovie(Movie movie);
}
