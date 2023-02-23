using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class MovieManager : IMovieService
{
    private readonly IRepositoryManager manager;
    private readonly ILoggerService logger;

    public MovieManager(IRepositoryManager manager, ILoggerService logger)
    {
        this.manager = manager;
        this.logger = logger;
    }
    public Movie CreateOneMovie(Movie movie)
    {
        manager.Movie.CreateOneMovie(movie);
        manager.Save();

        return movie;
    }

    public void DeleteOneMovie(int id, bool trackChanges)
    {
        // check entity
        var entity = manager.Movie.GetOneMovieById(id, trackChanges);
        if (entity is null)
        {
            string message = $"Movie with id:{id} could not found!";
            logger.LogInfo(message);
            throw new Exception(message);
        }

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
        {
            string message = $"Movie with id:{id} could not found!";
            logger.LogInfo(message);
            throw new Exception(message);
        }

        entity.Name = movie.Name;
        entity.Price = movie.Price;
        entity.ReleaseYear = movie.ReleaseYear;
        entity.Genre = movie.Genre;
        manager.Save();

    }
}
