using System;
using System.Linq;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)

        UpdateGenreCommand command = new(context);
        command.GenreId = 999;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No genre found to be updated");
    }

    [Fact]
    public void WhenGivenGenreNameAlreadyExists_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        UpdateGenreCommand command = new(context);
        var genre = new Genre { Name = "Deneme" };

        context.Genres.Add(genre);
        context.SaveChanges();

        var genre1 = new Genre { Name = "Test" };

        context.Genres.Add(genre1);
        context.SaveChanges();

        command.GenreId = genre1.Id;
        UpdateGenreCommand.UpdateGenreModel model = new UpdateGenreCommand.UpdateGenreModel { Name = "Deneme" };
        command.Model = model;

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("a book genre with the same name already exists");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        // arrange
        UpdateGenreCommand command = new(context);
        var genre = new Genre { Name = "Deneme" };

        context.Genres.Add(genre);
        context.SaveChanges();

        command.GenreId = genre.Id;
        UpdateGenreCommand.UpdateGenreModel model = new UpdateGenreCommand.UpdateGenreModel { Name = "Yeni Test" };
        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var updatedGenre = context.Genres.SingleOrDefault(g => g.Id == genre.Id);
        updatedGenre.Should().NotBeNull();
        updatedGenre.IsActive.Should().BeTrue();
        updatedGenre.Name.Should().Be(model.Name);
    }
}
