using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExitsAuthorFullNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var author = new Author()
        {
            Name = "Yusuf",
            Surname = "Kızılkaya",
            Birthday = new DateTime(1994, 08, 07)
        };
        context.Authors.Add(author);
        context.SaveChanges();

        CreateAuthorCommand command = new(context, mapper);
        command.Model = new CreateAuthorCommand.CreateAuthorModel { Name = author.Name, Surname = author.Surname, Birthday = author.Birthday };

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("That Author already exists");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        // arrange
        CreateAuthorCommand command = new(context, mapper);
        CreateAuthorCommand.CreateAuthorModel model = new CreateAuthorCommand.CreateAuthorModel()
        {
            Name = "Mustafa",
            Surname = "Kızılkaya",
            Birthday = new DateTime(1994, 08, 07)
        };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var author = context.Authors.SingleOrDefault(g => g.Name == model.Name);
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
        author.Surname.Should().Be(model.Surname);
        author.Birthday.Should().Be(model.Birthday);
    }
}
