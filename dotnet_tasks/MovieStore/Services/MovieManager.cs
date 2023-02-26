using System.Dynamic;
using AutoMapper;
using Entities.Dtos;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class MovieManager : IMovieService
{
    private readonly IRepositoryManager manager;
    private readonly ILoggerService logger;
    private readonly IMapper mapper;
    private readonly IDataShaper<MovieDto> shaper;

    public MovieManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<MovieDto> shaper)
    {
        this.shaper = shaper;
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
        var entity = await GetOneMovieByIdAndCheckExists(id, trackChanges);

        manager.Movie.DeleteOneMovie(entity);
        await manager.SaveAsync();
    }

    public async Task<(IEnumerable<ExpandoObject> movies, MetaData metaData)> GetAllMoviesAsync(MovieParameters movieParameters, bool trackChanges)
    {
        if (!movieParameters.ValidPriceRange)
            throw new PriceOutOfRangeBadRequestException();

        var moviesWithMetaData = await manager.Movie.GetAllMoviesAsync(movieParameters, trackChanges);
        var moviesDto = mapper.Map<IEnumerable<MovieDto>>(moviesWithMetaData);

        var shapedData = shaper.ShapeData(moviesDto, movieParameters.Fields);
        return (shapedData, moviesWithMetaData.MetaData);
    }

    public async Task<MovieDto> GetOneMovieByIdAsync(int id, bool trackChanges)
    {
        var movie = await GetOneMovieByIdAndCheckExists(id, trackChanges);

        return mapper.Map<MovieDto>(movie);
    }

    public async Task<(MovieDtoForUpdate movieDtoForUpdate, Movie movie)> GetOneMovieForPatchAsync(int id, bool trackChanges)
    {
        var movie = await GetOneMovieByIdAndCheckExists(id, trackChanges);

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
        var entity = await GetOneMovieByIdAndCheckExists(id, trackChanges);

        mapper.Map<Movie>(movieDto);

        await manager.SaveAsync();

    }

    private async Task<Movie> GetOneMovieByIdAndCheckExists(int id, bool trackChanges)
    {
        // check entity
        var entity = await manager.Movie.GetOneMovieByIdAsync(id, trackChanges);
        if (entity is null)
            throw new MovieNotFoundException(id);

        return entity;
    }
}
