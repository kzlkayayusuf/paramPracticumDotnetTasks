using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExitsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var book = new Book()
        {
            Title = "WhenAlreadyExitsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
            PageCount = 100,
            PublishDate = new DateTime(1990, 01, 10),
            GenreId = 1,
            AuthorId = 1
        };
        context.Books.Add(book);
        context.SaveChanges();

        CreateBookCommand command = new(context, mapper);
        command.Model = new CreateBookCommand.CreateBookModel() { Title = book.Title };

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("That Book already exists");

        // act (Çalıştırma)

        // assert (Doğrulama)
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        // arrange
        CreateBookCommand command = new(context, mapper);
        CreateBookModel model = new CreateBookModel()
        {
            Title = "Hobbit",
            PageCount = 1000,
            PublishDate = DateTime.Now.Date.AddYears(-11),
            GenreId = 1,
            AuthorId = 3
        };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var book = context.Books.SingleOrDefault(b => b.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        //book.Title.Should().Be(model.Title);
        book.GenreId.Should().Be(model.GenreId);
        book.AuthorId.Should().Be(model.AuthorId);
    }
}
