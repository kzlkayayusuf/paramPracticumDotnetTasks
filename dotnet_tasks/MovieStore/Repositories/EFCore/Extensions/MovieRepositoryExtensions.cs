using System.Reflection;
using System.Text;
using Entities.Models;
using System.Linq.Dynamic.Core;

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

    public static IQueryable<Movie> Sort(this IQueryable<Movie> movies, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return movies.OrderBy(m => m.Id);

        var orderParams = orderByQueryString.Trim().Split(',');

        // reflection
        var propertyInfos = typeof(Movie).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(' ')[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName,
                StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty is null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

        if (orderQuery is null)
            return movies.OrderBy(m => m.Id);

        return movies.OrderBy(orderQuery);
    }
}
