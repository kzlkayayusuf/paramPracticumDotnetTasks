using System;
using System.Linq;

namespace WebApi.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommand
{
    private readonly IBookStoreDbContext dbContext;
    public int BookId { get; set; }

    public DeleteBookCommand(IBookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if (book is null)
            throw new InvalidOperationException("The book to be deleted was not found");

        dbContext.Books.Remove(book);
        dbContext.SaveChanges();
    }
}
