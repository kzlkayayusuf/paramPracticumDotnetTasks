using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public interface IMovieService
{
    IEnumerable<MovieDto> GetAllMovies(bool trackChanges);
    MovieDto GetOneMovieById(int id, bool trackChanges);
    MovieDto CreateOneMovie(MovieDtoForInsertion movie);
    void UpdateOneMovie(int id, MovieDtoForUpdate movieDto, bool trackChanges);
    void DeleteOneMovie(int id, bool trackChanges);
    (MovieDtoForUpdate movieDtoForUpdate, Movie movie) GetOneMovieForPatch(int id, bool trackChanges);
    void SaveChangesForPatch(MovieDtoForUpdate movieDtoForUpdate, Movie movie);
}
