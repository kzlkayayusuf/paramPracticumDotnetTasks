using Entities.Models;

namespace Repositories.EFCore;

public static class MovieRepositoryExtensions
{
    public static IQueryable<Movie> FilterMovies(this IQueryable<Movie> movies,
        uint minPrice, uint maxPrice) =>
        movies.Where(movie => (movie.Price >= minPrice) && (movie.Price <= maxPrice));
}
