using System;
using System.Linq;
using AutoMapper;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    public int GenreId { get; set; }
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("Book genre not found");
        return mapper.Map<GenreDetailViewModel>(genre);
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
