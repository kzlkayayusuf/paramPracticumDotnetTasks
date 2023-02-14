using System.Collections.Generic;
using WebAPI.Application.CharacterOperations.Commands.CreateCharacter;
using WebAPI.Application.CharacterOperations.Commands.UpdateCharacter;
using WebAPI.Application.CharacterOperations.Queries.GetCharacterByName;
using WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail;
using WebAPI.Application.CharacterOperations.Queries.GetCharacters;

namespace WebAPI.Services.CartoonCharacterService;

public interface ICartoonCharacterService
{
    ServiceResponse<List<GetCharactersQuery.CharactersViewModel>> GetAllCharacters();
    ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> GetCharacterById(int id);
    ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> AddCharacter(CreateCharacterCommand.CreateCharacterModel newCharacter);
    ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> UpdateCharacter(int id, UpdateCharacterCommand.UpdateCharacterModel character);
    ServiceResponse<GetCharacterDetailQuery.CharacterDetailViewModel> DeleteCharacter(int id);
    ServiceResponse<GetCharacterByNameQuery.GetCharacterByNameViewModel> GetCharacterByName(string name);
}
