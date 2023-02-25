using Entities.Dtos;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts;

public interface IMovieService
{
    Task<IEnumerable<MovieDto>> GetAllMoviesAsync(MovieParameters movieParameters, bool trackChanges);
    Task<MovieDto> GetOneMovieByIdAsync(int id, bool trackChanges);
    Task<MovieDto> CreateOneMovieAsync(MovieDtoForInsertion movie);
    Task UpdateOneMovieAsync(int id, MovieDtoForUpdate movieDto, bool trackChanges);
    Task DeleteOneMovieAsync(int id, bool trackChanges);
    Task<(MovieDtoForUpdate movieDtoForUpdate, Movie movie)> GetOneMovieForPatchAsync(int id, bool trackChanges);
    Task SaveChangesForPatchAsync(MovieDtoForUpdate movieDtoForUpdate, Movie movie);
}
