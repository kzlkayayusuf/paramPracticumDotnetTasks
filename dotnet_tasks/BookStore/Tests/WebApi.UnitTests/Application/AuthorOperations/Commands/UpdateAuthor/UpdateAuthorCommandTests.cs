using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        UpdateAuthorCommand command = new(context);
        command.AuthorId = 999;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No author found to be updated");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
    {
        // arrange
        UpdateAuthorCommand command = new(context);
        var author = new Author { Name = "Leyla", Surname = "Soylu", Birthday = new DateTime(2000, 11, 22) };

        context.Authors.Add(author);
        context.SaveChanges();

        command.AuthorId = author.Id;
        UpdateAuthorCommand.UpdateAuthorModel model = new UpdateAuthorCommand.UpdateAuthorModel { Name = "Ayşe", Surname = "Kabuk", Birthday = new DateTime(2002, 11, 22) };
        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var updatedAuthor = context.Authors.SingleOrDefault(a => a.Id == author.Id);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.Name.Should().Be(model.Name);
        updatedAuthor.Surname.Should().Be(model.Surname);
        updatedAuthor.Birthday.Should().Be(model.Birthday);
    }
}
