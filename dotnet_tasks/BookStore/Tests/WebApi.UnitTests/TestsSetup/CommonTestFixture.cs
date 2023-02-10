using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestsSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase("BookStoreTestDb").Options;
        Context = new(options);
        Context.Database.EnsureCreated();
        Context.AddBooks();
        Context.AddAuthors();
        Context.AddGenres();
        Context.SaveChanges();

        Mapper = new MapperConfiguration(config => { config.AddProfile<AutoMapperProfile>(); }).CreateMapper();
    }

}
