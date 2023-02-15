using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private DeleteGenreCommandValidator _validator;

    public DeleteGenreCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenGenreIdLessThanOrEqualZero_ValidationShouldReturnError(int genreId)
    {
        // arrange
        DeleteGenreCommand command = new(null);
        command.GenreId = genreId;

        // act
        var result = _validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        // arrange
        DeleteGenreCommand command = new(null);
        command.GenreId = 12;

        // act
        var result = _validator.Validate(command);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
