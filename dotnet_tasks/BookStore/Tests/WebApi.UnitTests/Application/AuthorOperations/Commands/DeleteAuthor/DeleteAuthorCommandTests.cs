using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        DeleteAuthorCommand command = new(context);
        command.AuthorId = 120;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author to be deleted was not found");
    }

    [Fact]
    public void WhenGivenAuthorHaveBook_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        DeleteAuthorCommand command = new(context);
        command.AuthorId = 1;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author have book(s) so you can't delete");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        // arrange
        var newAuthor = new Author()
        {
            Name = "Yusuf",
            Surname = "Kızılkaya",
            Birthday = new DateTime(1994, 08, 07)
        };
        context.Authors.Add(newAuthor);
        context.SaveChanges();

        DeleteAuthorCommand command = new(context);
        command.AuthorId = newAuthor.Id;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var author = context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
        author.Should().BeNull();
    }
}
