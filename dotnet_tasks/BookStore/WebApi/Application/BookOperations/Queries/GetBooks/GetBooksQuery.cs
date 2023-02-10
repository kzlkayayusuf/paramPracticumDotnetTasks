using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBooks;

public class GetBooksQuery
{
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;

    public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(b => b.Id).ToList<Book>();
        List<BooksViewModel> vm = mapper.Map<List<BooksViewModel>>(bookList); //new List<BooksViewModel>();

        /*
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
        */
        return vm;
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}

