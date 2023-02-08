using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.CreateBooks;
using WebApi.BookOperations.GetBooks;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private readonly BookStoreDbContext context;

    public BookController(BookStoreDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(context);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = context.Books.Where(b => b.Id == id).SingleOrDefault();
        return book;
    }

    /*
        [HttpGet]
        public Book Get([FromQuery]string id)
        {
            var book = BookList.Where(b => b.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }
        */

    // Post
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBooksModel newBook)
    {
        CreateBooksQuery query = new CreateBooksQuery(context);
        try
        {
            query.Model = newBook;
            query.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
    //Put
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();

        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        context.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = context.Books.SingleOrDefault(x => x.Id == id);
        if (book is null)
            return BadRequest();

        context.Books.Remove(book);
        context.SaveChanges();
        return Ok();
    }
}
