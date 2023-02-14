using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail;

public class GetCharacterDetailQuery
{
    public int CharacterId { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCharacterDetailQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CharacterDetailViewModel> Handle()
    {
        var character = context.CartoonCharacters.Include(x => x.Cartoon).Where(c => c.ID == CharacterId).SingleOrDefault();
        if (character is null)
            throw new Exception($"Character with Id '{CharacterId}' not found.");
        CharacterDetailViewModel vm = mapper.Map<CharacterDetailViewModel>(character);

        return new ServiceResponse<CharacterDetailViewModel>(vm);
    }

    public class CharacterDetailViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CartoonName { get; set; }
    }
}
