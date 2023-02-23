using Entities.Models;

namespace Services.Contracts;

public interface IMovieService
{
    IEnumerable<Movie> GetAllMovies(bool trackChanges);
    Movie GetOneMovieById(int id, bool trackChanges);
    Movie CreateOneMovie(Movie movie);
    void UpdateOneMovie(int id, Movie movie, bool trackChanges);
    void DeleteOneMovie(int id, bool trackChanges);
}
