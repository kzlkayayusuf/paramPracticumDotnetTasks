using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private UpdateGenreCommandValidator _validator;

    public UpdateGenreCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData("1")]
    [InlineData("x")]
    [InlineData("123")]
    [InlineData("ab")]
    public void WhenModelIsInvalid_Validator_ShouldHaveError(string name)
    {
        // arrange
        var model = new UpdateGenreCommand.UpdateGenreModel { Name = name };
        UpdateGenreCommand updateCommand = new(null);
        updateCommand.GenreId = 1;
        updateCommand.Model = model;

        // act
        var result = _validator.Validate(updateCommand);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("Deneme1")]
    [InlineData("Deneme Test")]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError(string name)
    {
        // arrange
        var model = new UpdateGenreCommand.UpdateGenreModel { Name = name };
        UpdateGenreCommand updateCommand = new(null);
        updateCommand.GenreId = 2;
        updateCommand.Model = model;

        // act
        var result = _validator.Validate(updateCommand);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
