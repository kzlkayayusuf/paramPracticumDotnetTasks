using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        DeleteBookCommand command = new(context);
        command.BookId = 12;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book to be deleted was not found");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
        // arrange
        DeleteBookCommand command = new(context);
        command.BookId = 1;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var book = context.Books.SingleOrDefault(b => b.Id == command.BookId);
        book.Should().BeNull();
    }
}
