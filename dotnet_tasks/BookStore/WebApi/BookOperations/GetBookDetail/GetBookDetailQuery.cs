using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace WebApi.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext dbContext;
    private readonly IMapper mapper;

    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = dbContext.Books.Where(b => b.Id == BookId).SingleOrDefault();
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
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
