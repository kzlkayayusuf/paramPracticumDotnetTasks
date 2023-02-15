using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooks;

public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBooksQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGetBooksQueryIsHandled_BookListShouldBeReturned()
    {
        // Arrange
        var query = new GetBooksQuery(context, mapper);

        // Act
        var result = query.Handle();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);

        result[0].Title.Should().Be("Lean Startup");
        result[0].Genre.Should().Be("Personel Growth");
        result[0].Author.Should().Be("Eric Ries");
        result[0].PageCount.Should().Be(200);
        result[0].PublishDate.Should().Be("12.06.2001 00:00:00");

        result[1].Title.Should().Be("Herland");
        result[1].Genre.Should().Be("Science Fiction");
        result[1].Author.Should().Be("Charlotte Perkins Gilman");
        result[1].PageCount.Should().Be(250);
        result[1].PublishDate.Should().Be("23.05.2010 00:00:00");

        result[2].Title.Should().Be("Dune");
        result[2].Genre.Should().Be("Science Fiction");
        result[2].Author.Should().Be("Frank Herbert");
        result[2].PageCount.Should().Be(540);
        result[2].PublishDate.Should().Be("21.12.2002 00:00:00");

    }
}
