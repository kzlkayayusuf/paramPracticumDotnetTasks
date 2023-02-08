using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext dbContext;

    public GetBooksQuery(BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = dbContext.Books.OrderBy(b => b.Id).ToList<Book>();
        List<BooksViewModel> vm = new List<BooksViewModel>();
        foreach (var book in bookList)
        {
            vm.Add(new BooksViewModel()
            {
                Title = book.Title,
                Genre = ((Genre)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                PageCount = book.PageCount
            });
        }
        return vm;
    }
}

public class BooksViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}