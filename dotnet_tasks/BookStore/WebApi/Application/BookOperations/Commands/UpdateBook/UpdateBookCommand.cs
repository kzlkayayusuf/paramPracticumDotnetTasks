using System;
using System.Linq;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommand
{
    private readonly IBookStoreDbContext dbContext;
    public int BookId { get; set; }
    public UpdateBookModel Model { get; set; }

    public UpdateBookCommand(IBookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if (book is null)
            throw new InvalidOperationException("No book found to be updated");

        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
        book.Title = Model.Title != default ? Model.Title : book.Title;

        dbContext.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}

