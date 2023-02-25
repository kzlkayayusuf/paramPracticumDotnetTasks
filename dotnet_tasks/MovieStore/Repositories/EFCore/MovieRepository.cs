using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(m => m.Id).ToListAsync();

    public async Task<Movie> GetOneMovieByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(m => m.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public void CreateOneMovie(Movie movie) => Create(movie);

    public void UpdateOneMovie(Movie movie) => Update(movie);

    public void DeleteOneMovie(Movie movie) => Delete(movie);
}