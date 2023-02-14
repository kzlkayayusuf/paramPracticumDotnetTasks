using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;
using static WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail.GetCharacterDetailQuery;

namespace WebAPI.Application.CharacterOperations.Commands.UpdateCharacter;

public class UpdateCharacterCommand
{
    public int CharacterId { get; set; }
    public UpdateCharacterModel Model { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public UpdateCharacterCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CharacterDetailViewModel> Handle()
    {
        var character = context.CartoonCharacters.Include(x => x.Cartoon).Where(c => c.ID == CharacterId).SingleOrDefault();
        if (character is null)
            throw new InvalidOperationException($"The character with Id '{CharacterId}' to be updated was not found");


        character.Name = Model.Name != default ? Model.Name : character.Name;
        character.CartoonID = Model.CartoonId != default ? Model.CartoonId : character.CartoonID;

        context.SaveChanges();

        CharacterDetailViewModel vm = mapper.Map<CharacterDetailViewModel>(character);
        return new ServiceResponse<CharacterDetailViewModel>(vm);
    }

    public class UpdateCharacterModel
    {
        public string Name { get; set; }
        public int CartoonId { get; set; }
    }
}
