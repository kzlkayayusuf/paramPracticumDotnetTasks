using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;

    public int BookId { get; set; }

    public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = dbContext.Books.Include(x => x.Genre).Include(x => x.Author).Where(b => b.Id == BookId).SingleOrDefault();
        if (book is null)
            throw new InvalidOperationException("The Book Not Found");
        BookDetailViewModel vm = mapper.Map<BookDetailViewModel>(book); //new BookDetailViewModel();
        //vm.Title = book.Title;
        //vm.PageCount = book.PageCount;
        //vm.Genre = ((Genre)book.GenreId).ToString();
        //vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        return vm;
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
