using System;
using System.Linq;
using AutoMapper;

namespace WebApi.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;

    public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var newBook = dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        if (newBook is not null)
            throw new InvalidOperationException("That Book already exists");

        newBook = mapper.Map<Book>(Model); //new Book();
        //newBook.Title = Model.Title;
        //newBook.PublishDate = Model.PublishDate;
        //newBook.PageCount = Model.PageCount;
        //newBook.GenreId = Model.GenreId;

        dbContext.Books.Add(newBook);
        dbContext.SaveChanges();
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }

}

