using System.Collections.Generic;
using FluentValidation;
using WebAPI.Application.CharacterOperations.Commands.CreateCharacter;
using WebAPI.Application.CharacterOperations.Commands.DeleteCharacter;
using WebAPI.Application.CharacterOperations.Commands.UpdateCharacter;
using WebAPI.Application.CharacterOperations.Queries.GetCharacterByName;
using WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail;
using WebAPI.Application.CharacterOperations.Queries.GetCharacters;
using WebAPI.DBOperations;

namespace WebAPI.Services.CartoonCharacterService;

public class CartoonCharacterService : ICartoonCharacterService
{
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public CartoonCharacterService(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<List<GetCharactersQuery.CharactersViewModel>> GetAllCharacters()
    {
        GetCharactersQuery query = new(context, mapper);


        return query.Handle();
    }

    public ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> GetCharacterById(int id)
    {
        GetCharacterDetailQuery query = new(context, mapper);
        query.CharacterId = id;
        GetCharacterDetailQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return query.Handle();
    }

    public ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> AddCharacter(CreateCharacterCommand.CreateCharacterModel newCharacter)
    {
        CreateCharacterCommand command = new(context, mapper);
        command.Model = newCharacter;

        CreateCharacterCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> DeleteCharacter(int id)
    {
        DeleteCharacterCommand command = new(context, mapper);
        command.CharacterId = id;
        DeleteCharacterCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> UpdateCharacter(int id, UpdateCharacterCommand.UpdateCharacterModel character)
    {
        UpdateCharacterCommand command = new(context, mapper);
        command.CharacterId = id;
        command.Model = character;

        UpdateCharacterCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<GetCharacterByNameQuery.GetCharacterByNameViewModel> GetCharacterByName(string name)
    {
        GetCharacterByNameQuery query = new(context, mapper);
        query.CharacterName = name;
        GetCharacterByNameQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return query.Handle();
    }
}
