using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class MovieManager : IMovieService
{
    private readonly IRepositoryManager manager;

    public MovieManager(IRepositoryManager manager)
    {
        this.manager = manager;
    }
    public Movie CreateOneMovie(Movie movie)
    {
        if (movie is null)
            throw new ArgumentNullException(nameof(movie));

        manager.Movie.CreateOneMovie(movie);
        manager.Save();

        return movie;
    }

    public void DeleteOneMovie(int id, bool trackChanges)
    {
        // check entity
        var entity = manager.Movie.GetOneMovieById(id, trackChanges);
        if (entity is null)
            throw new Exception($"Movie with id:{id} could not found!");

        manager.Movie.DeleteOneMovie(entity);
        manager.Save();
    }

    public IEnumerable<Movie> GetAllMovies(bool trackChanges)
    {
        return manager.Movie.GetAllMovies(trackChanges);
    }

    public Movie GetOneMovieById(int id, bool trackChanges)
    {
        return manager.Movie.GetOneMovieById(id, trackChanges);
    }

    public void UpdateOneMovie(int id, Movie movie, bool trackChanges)
    {
        // check entity
        var entity = manager.Movie.GetOneMovieById(id, trackChanges);
        if (entity is null)
            throw new Exception($"Movie with id:{id} could not found!");

        // check params
        if (movie is null)
            throw new ArgumentNullException(nameof(movie));

        entity.Name = movie.Name;
        entity.Price = movie.Price;
        entity.ReleaseYear = movie.ReleaseYear;
        entity.Genre = movie.Genre;
        manager.Save();

    }
}
