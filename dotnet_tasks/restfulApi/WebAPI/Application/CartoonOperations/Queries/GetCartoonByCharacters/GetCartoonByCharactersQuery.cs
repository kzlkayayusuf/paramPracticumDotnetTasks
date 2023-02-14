using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonByCharacters;

public class GetCartoonByCharactersQuery
{
    public List<string> CartoonCharacters { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCartoonByCharactersQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<List<GetCartoonByCharactersViewModel>> Handle()
    {
        //var cartoon = context.Cartoons.Include(x => x.Characters).Where(c => c.Characters.Any(ch => CartoonCharacters.Any(character => ch.Name.Equals(character, StringComparison.OrdinalIgnoreCase))));

        //var cartoon = context.Cartoons.Include(x => x.Characters).Where(c => c.Characters.Any(ch => CartoonCharacters.Select(cc => cc.ToString()).Contains(ch, StringComparer.OrdinalIgnoreCase)));
        //var cartoon = context.Cartoons.Include(x => x.Characters).Where(c => c.Characters.Any(ch => CartoonCharacters.Any(c => c.Equals(ch.Name, StringComparison.OrdinalIgnoreCase)))).SingleOrDefault();
        var cartoon = context.Cartoons.Include(x => x.Characters)
                              .ToList()
                              .Where(c => c.Characters.Any(ch => CartoonCharacters.Any(c => c.Equals(ch.Name, StringComparison.OrdinalIgnoreCase))))
                              .ToList<Cartoon>();
        if (cartoon is null)
            throw new Exception($"Cartoon with characters '{CartoonCharacters.ToString()}' not found.");
        List<GetCartoonByCharactersViewModel> vm = mapper.Map<List<GetCartoonByCharactersViewModel>>(cartoon);

        return new ServiceResponse<List<GetCartoonByCharactersViewModel>>(vm);
    }

    public class GetCartoonByCharactersViewModel
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public string ReleaseDate { get; set; }
        public string Topic { get; set; }
        public List<string> Characters { get; set; }
    }
}
