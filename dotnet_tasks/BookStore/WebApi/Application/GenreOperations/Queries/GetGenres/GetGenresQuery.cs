using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace WebApi.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genres = context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
        List<GenresViewModel> returnObj = mapper.Map<List<GenresViewModel>>(genres);
        return returnObj;
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
