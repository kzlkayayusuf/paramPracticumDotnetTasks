using AutoMapper;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static WebApi.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static WebApi.Application.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace WebApi.Common;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)); ;
        CreateMap<Entities.Genre, GenresViewModel>();
        CreateMap<Entities.Genre, GenreDetailViewModel>();
        CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
        CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
        CreateMap<CreateAuthorModel, Author>();
    }
}
