using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;
using static WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail.GetCharacterDetailQuery;

namespace WebAPI.Application.CharacterOperations.Commands.CreateCharacter;

public class CreateCharacterCommand
{
    public CreateCharacterModel Model { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public CreateCharacterCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CharacterDetailViewModel> Handle()
    {
        var newCharacter = context.CartoonCharacters.FirstOrDefault(c => c.Name.ToLower() == Model.Name.ToLower());
        if (newCharacter is not null)
            throw new InvalidOperationException("That Character already exists");

        if (Model.CartoonId > context.Cartoons.Count())
            throw new InvalidOperationException("Cartoon Id not exists!");

        newCharacter = mapper.Map<CartoonCharacter>(Model);

        context.CartoonCharacters.Add(newCharacter);
        context.SaveChanges();

        newCharacter = context.CartoonCharacters.Include(x => x.Cartoon).Where(c => c.Name.ToLower() == Model.Name.ToLower()).FirstOrDefault();
        CharacterDetailViewModel vm = mapper.Map<CharacterDetailViewModel>(newCharacter);
        return new ServiceResponse<CharacterDetailViewModel>(vm);
    }

    public class CreateCharacterModel
    {
        public string Name { get; set; }
        public int CartoonId { get; set; }
    }
}
