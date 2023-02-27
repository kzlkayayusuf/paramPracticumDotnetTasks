using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore;

public sealed class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<PagedList<Movie>> GetAllMoviesAsync(MovieParameters movieParameters, bool trackChanges)
    {
        var movies = await FindAll(trackChanges)
        .FilterMovies(movieParameters.MinPrice, movieParameters.MaxPrice)
        .Search(movieParameters.SearchTerm)
        .Sort(movieParameters.OrderBy)
        .ToListAsync();

        return PagedList<Movie>.ToPagedList(movies, movieParameters.PageNumber, movieParameters.PageSize);
    }

    public async Task<Movie> GetOneMovieByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(m => m.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void CreateOneMovie(Movie movie) => Create(movie);

    public void UpdateOneMovie(Movie movie) => Update(movie);

    public void DeleteOneMovie(Movie movie) => Delete(movie);

    public async Task<List<Movie>> GetAllMoviesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(m => m.Id).ToListAsync();
    }
}