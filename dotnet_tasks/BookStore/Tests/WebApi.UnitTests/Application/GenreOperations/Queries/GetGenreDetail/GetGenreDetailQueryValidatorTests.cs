using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    private GetGenreDetailQueryValidator _validator;

    public GetGenreDetailQueryValidatorTests()
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
        GetGenreDetailQuery query = new(null, null);
        query.GenreId = genreId;

        // act
        var result = _validator.Validate(query);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        // arrange
        GetGenreDetailQuery query = new(null, null);
        query.GenreId = 12;

        // act
        var result = _validator.Validate(query);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
