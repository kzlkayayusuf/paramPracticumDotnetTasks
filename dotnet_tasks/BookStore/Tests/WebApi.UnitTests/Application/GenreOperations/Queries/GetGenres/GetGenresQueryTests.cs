using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenresQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGetGenresQueryIsHandled_GenreListShouldBeReturned()
    {
        // Arrange
        var query = new GetGenresQuery(context, mapper);

        // Act
        var result = query.Handle();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);

        result[0].Name.Should().Be("Personel Growth");

        result[1].Name.Should().Be("Science Fiction");

        result[2].Name.Should().Be("Romance");

    }
}
