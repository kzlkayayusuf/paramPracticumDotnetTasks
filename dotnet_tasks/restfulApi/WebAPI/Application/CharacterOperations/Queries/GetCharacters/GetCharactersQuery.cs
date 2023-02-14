using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CharacterOperations.Queries.GetCharacters;

public class GetCharactersQuery
{
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCharactersQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<List<CharactersViewModel>> Handle()
    {
        var characterList = context.CartoonCharacters.Include(x => x.Cartoon).OrderBy(c => c.ID).ToList<CartoonCharacter>();
        List<CharactersViewModel> vm = mapper.Map<List<CharactersViewModel>>(characterList);

        return new ServiceResponse<List<CharactersViewModel>>(vm);
    }

    public class CharactersViewModel
    {
        public string Name { get; set; }
        public string CartoonName { get; set; }
    }
}
