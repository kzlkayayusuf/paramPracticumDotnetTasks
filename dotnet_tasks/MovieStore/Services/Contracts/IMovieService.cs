using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public interface IMovieService
{
    IEnumerable<Movie> GetAllMovies(bool trackChanges);
    Movie GetOneMovieById(int id, bool trackChanges);
    Movie CreateOneMovie(Movie movie);
    void UpdateOneMovie(int id, MovieDtoForUpdate movieDto, bool trackChanges);
    void DeleteOneMovie(int id, bool trackChanges);
}
