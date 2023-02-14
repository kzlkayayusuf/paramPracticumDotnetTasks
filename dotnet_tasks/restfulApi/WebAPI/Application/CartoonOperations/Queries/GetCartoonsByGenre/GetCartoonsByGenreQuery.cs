using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonsByGenre;

public class GetCartoonsByGenreQuery
{
    public Genre CartoonGenre { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCartoonsByGenreQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<List<GetCartoonsByGenreViewModel>> Handle()
    {
        var cartoonGenreList = context.Cartoons.Include(x => x.Characters).Where(c => c.Genre.ToString().ToLower() == CartoonGenre.ToString().ToLower()).OrderByDescending(c => c.ID).ToList<Cartoon>();
        if (cartoonGenreList is null)
            throw new Exception($"Cartoon with Genre '{CartoonGenre}' not found.");
        List<GetCartoonsByGenreViewModel> vm = mapper.Map<List<GetCartoonsByGenreViewModel>>(cartoonGenreList);

        return new ServiceResponse<List<GetCartoonsByGenreViewModel>>(vm);
    }

    public class GetCartoonsByGenreViewModel
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public string ReleaseDate { get; set; }
        public string Topic { get; set; }
        public List<string> Characters { get; set; }
    }
}
