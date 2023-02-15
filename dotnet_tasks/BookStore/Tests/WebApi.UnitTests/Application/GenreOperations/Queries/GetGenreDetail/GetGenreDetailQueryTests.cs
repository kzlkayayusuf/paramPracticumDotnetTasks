using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestsSetup;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
    {
        // arrange
        GetGenreDetailQuery query = new(context, mapper);
        var GenreId = query.GenreId = 1;

        var genre = context.Genres.Where(g => g.Id == GenreId).SingleOrDefault();

        // act
        GenreDetailViewModel vm = query.Handle();

        // assert
        vm.Should().NotBeNull();
        vm.Name.Should().Be(genre.Name);
    }

    [Fact]
    public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        int genreId = 11;

        GetGenreDetailQuery query = new(context, mapper);
        query.GenreId = genreId;

        // assert
        query.Invoking(x => x.Handle())
             .Should().Throw<InvalidOperationException>()
             .And.Message.Should().Be("Book genre not found");
    }
}
