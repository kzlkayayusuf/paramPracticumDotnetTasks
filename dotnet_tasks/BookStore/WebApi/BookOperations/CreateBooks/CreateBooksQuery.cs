using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.BookOperations.CreateBooks;

public class CreateBooksQuery
{
    public CreateBooksModel Model { get; set; }
    private readonly BookStoreDbContext dbContext;

    public CreateBooksQuery(BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var newBook = dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        if (newBook is not null)
            throw new InvalidOperationException("That Book already exists");
        newBook = new Book();
        newBook.Title = Model.Title;
        newBook.PublishDate = Model.PublishDate;
        newBook.PageCount = Model.PageCount;
        newBook.GenreId = Model.GenreId;

        dbContext.Books.Add(entity: newBook);
        dbContext.SaveChanges();
    }
}

public class CreateBooksModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}