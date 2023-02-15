using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestsSetup;
using static WebApi.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeReturned()
    {
        // arrange
        GetAuthorDetailQuery query = new(context, mapper);
        var AuthorId = query.AuthorId = 1;

        var author = context.Authors.Where(a => a.Id == AuthorId).SingleOrDefault();

        // act
        AuthorDetailViewModel vm = query.Handle();

        // assert
        vm.Should().NotBeNull();
        vm.FullName.Should().Be(author.Name + " " + author.Surname);
        vm.Birthday.Should().Be(author.Birthday.ToString());
    }

    [Fact]
    public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        int authorId = 11;

        GetAuthorDetailQuery query = new(context, mapper);
        query.AuthorId = authorId;

        // assert
        query.Invoking(x => x.Handle())
             .Should().Throw<InvalidOperationException>()
             .And.Message.Should().Be("The Author not found");
    }
}
