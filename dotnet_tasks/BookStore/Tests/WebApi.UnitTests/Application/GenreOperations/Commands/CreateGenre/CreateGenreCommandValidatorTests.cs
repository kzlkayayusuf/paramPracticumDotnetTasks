using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestsSetup;
using static WebApi.Application.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("a")]
    [InlineData("abc")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
    {
        // arrange
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = name };

        CreateGenreCommandValidator validator = new();

        // act
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("abcd")]
    [InlineData("action")]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(string name)
    {
        // arrange
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = name };

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

        // act
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
