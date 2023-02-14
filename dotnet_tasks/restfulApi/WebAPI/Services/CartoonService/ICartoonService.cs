using System.Collections.Generic;
using WebAPI.Application.CartoonOperations.Commands.CreateCartoon;
using WebAPI.Application.CartoonOperations.Commands.UpdateCartoon;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonByCharacters;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonByName;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail;
using WebAPI.Application.CartoonOperations.Queries.GetCartoons;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonsByGenre;

namespace WebAPI.Services.CartoonService;

public interface ICartoonService
{
    ServiceResponse<List<GetCartoonsQuery.CartoonsViewModel>> GetAllCartoons();
    ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> GetCartoonById(int id);
    ServiceResponse<GetCartoonByNameQuery.GetCartoonByNameViewModel> GetCartoonByName(string name);
    ServiceResponse<List<GetCartoonsByGenreQuery.GetCartoonsByGenreViewModel>> GetCartoonsByGenre(Genre genre);
    ServiceResponse<List<GetCartoonByCharactersQuery.GetCartoonByCharactersViewModel>> GetCartoonByCharacters(List<string> characters);
    ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> AddCartoon(CreateCartoonCommand.CreateCartoonModel newCartoon);
    ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> UpdateCartoon(int id, UpdateCartoonCommand.UpdateCartoonModel cartoon);
    ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> DeleteCartoon(int id);
}
