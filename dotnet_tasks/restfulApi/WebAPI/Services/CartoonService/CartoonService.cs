using System.Collections.Generic;
using FluentValidation;
using WebAPI.Application.CartoonOperations.Commands.CreateCartoon;
using WebAPI.Application.CartoonOperations.Commands.DeleteCartoon;
using WebAPI.Application.CartoonOperations.Commands.UpdateCartoon;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonByCharacters;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonByName;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail;
using WebAPI.Application.CartoonOperations.Queries.GetCartoons;
using WebAPI.Application.CartoonOperations.Queries.GetCartoonsByGenre;
using WebAPI.DBOperations;

namespace WebAPI.Services.CartoonService;

public class CartoonService : ICartoonService
{
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public CartoonService(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> AddCartoon(CreateCartoonCommand.CreateCartoonModel newCartoon)
    {
        CreateCartoonCommand command = new CreateCartoonCommand(context, mapper);
        command.Model = newCartoon;

        CreateCartoonCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> DeleteCartoon(int id)
    {
        DeleteCartoonCommand command = new DeleteCartoonCommand(context, mapper);
        command.CartoonId = id;
        DeleteCartoonCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }

    public ServiceResponse<List<GetCartoonsQuery.CartoonsViewModel>> GetAllCartoons()
    {
        GetCartoonsQuery query = new GetCartoonsQuery(context, mapper);


        return query.Handle();
    }

    public ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> GetCartoonById(int id)
    {
        GetCartoonDetailQuery query = new GetCartoonDetailQuery(context, mapper);
        query.CartoonId = id;
        GetCartoonDetailQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return query.Handle();
    }

    public ServiceResponse<GetCartoonByNameQuery.GetCartoonByNameViewModel> GetCartoonByName(string name)
    {
        GetCartoonByNameQuery query = new GetCartoonByNameQuery(context, mapper);
        query.CartoonName = name;
        GetCartoonByNameQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return query.Handle();
    }

    public ServiceResponse<List<GetCartoonByCharactersQuery.GetCartoonByCharactersViewModel>> GetCartoonByCharacters(List<string> characters)
    {
        GetCartoonByCharactersQuery query = new GetCartoonByCharactersQuery(context, mapper);
        query.CartoonCharacters = characters;
        GetCartoonByCharactersQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return query.Handle();
    }


    public ServiceResponse<List<GetCartoonsByGenreQuery.GetCartoonsByGenreViewModel>> GetCartoonsByGenre(Genre genre)
    {
        GetCartoonsByGenreQuery query = new GetCartoonsByGenreQuery(context, mapper);
        query.CartoonGenre = genre;
        GetCartoonsByGenreQueryValidator validator = new();
        validator.ValidateAndThrow(query);

        return query.Handle();
    }

    public ServiceResponse<GetCartoonDetailQuery.CartoonDetailViewModel> UpdateCartoon(int id, UpdateCartoonCommand.UpdateCartoonModel cartoon)
    {
        UpdateCartoonCommand command = new UpdateCartoonCommand(context, mapper);
        command.CartoonId = id;
        command.Model = cartoon;

        UpdateCartoonCommandValidator validator = new();
        validator.ValidateAndThrow(command);

        return command.Handle();
    }
}
