using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestsSetup;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
    {
        // arrange
        GetBookDetailQuery query = new(context, mapper);
        var BookId = query.BookId = 1;

        var book = context.Books.Include(x => x.Genre).Include(x => x.Author).Where(b => b.Id == BookId).SingleOrDefault();

        // act
        BookDetailViewModel vm = query.Handle();

        // assert
        vm.Should().NotBeNull();
        vm.Title.Should().Be(book.Title);
        vm.PageCount.Should().Be(book.PageCount);
        vm.Genre.Should().Be(book.Genre.Name);
        vm.Author.Should().Be(book.Author.Name + " " + book.Author.Surname);
        vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));
    }

    [Fact]
    public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        int bookId = 11;

        GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
        query.BookId = bookId;

        // assert
        query.Invoking(x => x.Handle())
             .Should().Throw<InvalidOperationException>()
             .And.Message.Should().Be("The Book Not Found");
    }
}

