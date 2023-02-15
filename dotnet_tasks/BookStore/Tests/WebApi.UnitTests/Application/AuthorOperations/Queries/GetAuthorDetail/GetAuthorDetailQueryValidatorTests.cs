using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    private GetAuthorDetailQueryValidator _validator;

    public GetAuthorDetailQueryValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenAuthorIdLessThanOrEqualZero_ValidationShouldReturnError(int authorId)
    {
        // arrange
        GetAuthorDetailQuery query = new(null, null);
        query.AuthorId = authorId;

        // act
        var result = _validator.Validate(query);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenAuthorIdGreaterThanZero_ValidationShouldNotReturnError()
    {
        // arrange
        GetAuthorDetailQuery query = new(null, null);
        query.AuthorId = 12;

        // act
        var result = _validator.Validate(query);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}
