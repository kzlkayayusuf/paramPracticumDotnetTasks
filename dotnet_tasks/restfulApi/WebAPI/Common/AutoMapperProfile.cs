using System.Linq;
using static WebAPI.Application.CartoonOperations.Commands.CreateCartoon.CreateCartoonCommand;
using static WebAPI.Application.CartoonOperations.Commands.UpdateCartoon.UpdateCartoonCommand;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoonByCharacters.GetCartoonByCharactersQuery;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoonByName.GetCartoonByNameQuery;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail.GetCartoonDetailQuery;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoons.GetCartoonsQuery;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoonsByGenre.GetCartoonsByGenreQuery;
using static WebAPI.Application.CharacterOperations.Commands.CreateCharacter.CreateCharacterCommand;
using static WebAPI.Application.CharacterOperations.Queries.GetCharacterByName.GetCharacterByNameQuery;
using static WebAPI.Application.CharacterOperations.Queries.GetCharacterDetail.GetCharacterDetailQuery;
using static WebAPI.Application.CharacterOperations.Queries.GetCharacters.GetCharactersQuery;
using static WebAPI.Application.UserOperations.Commands.CreateUser.CreateUserCommand;
using static WebAPI.Application.UserOperations.Queries.GetUserDetail.GetUserDetailQuery;
using static WebAPI.Application.UserOperations.Queries.GetUsers.GetUsersQuery;

namespace WebAPI.Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCartoonModel, Cartoon>();
        CreateMap<UpdateCartoonModel, Cartoon>();
        CreateMap<Cartoon, CartoonsViewModel>().ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => c.Name)));
        CreateMap<Cartoon, GetCartoonsByGenreViewModel>().ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => c.Name)));
        CreateMap<Cartoon, GetCartoonByCharactersViewModel>().ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => c.Name)));
        CreateMap<Cartoon, CartoonDetailViewModel>().ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => c.Name)));
        CreateMap<Cartoon, GetCartoonByNameViewModel>().ForMember(dest => dest.Characters, opt => opt.MapFrom(src => src.Characters.Select(c => c.Name)));

        CreateMap<CartoonCharacter, CharactersViewModel>().ForMember(dest => dest.CartoonName, opt => opt.MapFrom(src => src.Cartoon.Name));
        CreateMap<CartoonCharacter, CharacterDetailViewModel>().ForMember(dest => dest.CartoonName, opt => opt.MapFrom(src => src.Cartoon.Name));
        CreateMap<CartoonCharacter, GetCharacterByNameViewModel>().ForMember(dest => dest.CartoonName, opt => opt.MapFrom(src => src.Cartoon.Name));
        CreateMap<CreateCharacterModel, CartoonCharacter>();

        CreateMap<User, UsersViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
        CreateMap<User, UserDetailViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname)); ;
        CreateMap<CreateUserModel, User>();
    }
}
