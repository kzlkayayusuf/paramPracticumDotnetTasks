using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;
using static WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail.GetCharacterDetailQuery;

namespace WebAPI.Application.CharacterOperations.Commands.DeleteCharacter;

public class DeleteCharacterCommand
{
    public int CharacterId { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public DeleteCharacterCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CharacterDetailViewModel> Handle()
    {
        var character = context.CartoonCharacters.Include(x => x.Cartoon).Where(c => c.ID == CharacterId).FirstOrDefault();

        if (character is null)
            throw new InvalidOperationException($"The character with Id '{CharacterId}' to be deleted was not found");

        CharacterDetailViewModel vm = mapper.Map<CharacterDetailViewModel>(character);

        context.CartoonCharacters.Remove(character);
        context.SaveChanges();

        return new ServiceResponse<CharacterDetailViewModel>(vm);
    }
}
