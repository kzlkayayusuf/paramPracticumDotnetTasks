using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Movie> GetAllMovies(bool trackChanges) => FindAll(trackChanges).OrderBy(m => m.Id);

    public Movie GetOneMovieById(int id, bool trackChanges) =>
        FindByCondition(m => m.Id.Equals(id), trackChanges).SingleOrDefault();

    public void CreateOneMovie(Movie movie) => Create(movie);

    public void UpdateOneMovie(Movie movie) => Update(movie);

    public void DeleteOneMovie(Movie movie) => Delete(movie);
}