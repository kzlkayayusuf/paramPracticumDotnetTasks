using Entities.Models;

namespace Repositories.EFCore.Extensions;

public static class MovieRepositoryExtensions
{
    public static IQueryable<Movie> FilterMovies(this IQueryable<Movie> movies,
        uint minPrice, uint maxPrice) =>
        movies.Where(movie => (movie.Price >= minPrice) && (movie.Price <= maxPrice));

    public static IQueryable<Movie> Search(this IQueryable<Movie> movies, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return movies;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return movies.Where(movie => movie.Name.ToLower().Contains(lowerCaseTerm));
    }
}
