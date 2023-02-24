using AutoMapper;
using Entities.Dtos;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class MovieManager : IMovieService
{
    private readonly IRepositoryManager manager;
    private readonly ILoggerService logger;
    private readonly IMapper mapper;

    public MovieManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
    {
        this.mapper = mapper;
        this.manager = manager;
        this.logger = logger;
    }
    public MovieDto CreateOneMovie(MovieDtoForInsertion movie)
    {
        var entity = mapper.Map<Movie>(movie);
        manager.Movie.CreateOneMovie(entity);
        manager.Save();

        return mapper.Map<MovieDto>(entity);
    }

    public void DeleteOneMovie(int id, bool trackChanges)
    {
        // check entity
        var entity = manager.Movie.GetOneMovieById(id, trackChanges);
        if (entity is null)
            throw new MovieNotFoundException(id);

        manager.Movie.DeleteOneMovie(entity);
        manager.Save();
    }

    public IEnumerable<MovieDto> GetAllMovies(bool trackChanges)
    {
        var movies = manager.Movie.GetAllMovies(trackChanges);
        return mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public MovieDto GetOneMovieById(int id, bool trackChanges)
    {
        var movie = manager.Movie.GetOneMovieById(id, trackChanges);
        if (movie is null)
            throw new MovieNotFoundException(id);

        return mapper.Map<MovieDto>(movie);
    }

    public void UpdateOneMovie(int id, MovieDtoForUpdate movieDto, bool trackChanges)
    {
        // check entity
        var entity = manager.Movie.GetOneMovieById(id, trackChanges);
        if (entity is null)
            throw new MovieNotFoundException(id);

        mapper.Map<Movie>(movieDto);

        manager.Save();

    }
}
