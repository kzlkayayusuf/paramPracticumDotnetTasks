using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CharacterOperations.Queries.GetCharacterByName;

public class GetCharacterByNameQuery
{
    public string CharacterName { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCharacterByNameQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<GetCharacterByNameViewModel> Handle()
    {
        var character = context.CartoonCharacters.Include(x => x.Cartoon).Where(c => c.Name.ToUpper().Contains(CharacterName.ToUpper())).SingleOrDefault();
        if (character is null)
            throw new Exception($"Character with Name '{CharacterName}' not found.");

        GetCharacterByNameViewModel vm = mapper.Map<GetCharacterByNameViewModel>(character);

        return new ServiceResponse<GetCharacterByNameViewModel>(vm);
    }

    public class GetCharacterByNameViewModel
    {
        public string Name { get; set; }
        public string CartoonName { get; set; }
    }
}
