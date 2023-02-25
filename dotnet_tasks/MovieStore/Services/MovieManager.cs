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
    public async Task<MovieDto> CreateOneMovieAsync(MovieDtoForInsertion movie)
    {
        var entity = mapper.Map<Movie>(movie);
        manager.Movie.CreateOneMovie(entity);

        await manager.SaveAsync();

        return mapper.Map<MovieDto>(entity);
    }

    public async Task DeleteOneMovieAsync(int id, bool trackChanges)
    {
        // check entity
        var entity = await manager.Movie.GetOneMovieByIdAsync(id, trackChanges);
        if (entity is null)
            throw new MovieNotFoundException(id);

        manager.Movie.DeleteOneMovie(entity);
        await manager.SaveAsync();
    }

    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync(bool trackChanges)
    {
        var movies = await manager.Movie.GetAllMoviesAsync(trackChanges);
        return mapper.Map<IEnumerable<MovieDto>>(movies);
    }

    public async Task<MovieDto> GetOneMovieByIdAsync(int id, bool trackChanges)
    {
        var movie = await manager.Movie.GetOneMovieByIdAsync(id, trackChanges);
        if (movie is null)
            throw new MovieNotFoundException(id);

        return mapper.Map<MovieDto>(movie);
    }

    public async Task<(MovieDtoForUpdate movieDtoForUpdate, Movie movie)> GetOneMovieForPatchAsync(int id, bool trackChanges)
    {
        var movie = await manager.Movie.GetOneMovieByIdAsync(id, trackChanges);
        if (movie is null)
            throw new MovieNotFoundException(id);

        var movieDtoForUpdate = mapper.Map<MovieDtoForUpdate>(movie);

        return (movieDtoForUpdate, movie);
    }

    public async Task SaveChangesForPatchAsync(MovieDtoForUpdate movieDtoForUpdate, Movie movie)
    {
        mapper.Map(movieDtoForUpdate, movie);
        await manager.SaveAsync();
    }

    public async Task UpdateOneMovieAsync(int id, MovieDtoForUpdate movieDto, bool trackChanges)
    {
        // check entity
        var entity = await manager.Movie.GetOneMovieByIdAsync(id, trackChanges);
        if (entity is null)
            throw new MovieNotFoundException(id);

        mapper.Map<Movie>(movieDto);

        await manager.SaveAsync();

    }
}
