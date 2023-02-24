using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace WebAPI.Utilities.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MovieDtoForUpdate, Movie>().ReverseMap();
        CreateMap<Movie, MovieDto>();
        CreateMap<MovieDtoForInsertion, Movie>();
    }
}
