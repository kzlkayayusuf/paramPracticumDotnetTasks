using WebApi.DBOperations;

namespace WebApi.UnitTests.TestsSetup;

public static class Genres
{
    public static void AddGenres(this BookStoreDbContext context)
    {
        context.Genres.AddRange
            (
                new Entities.Genre
                {
                    Name = "Personel Growth"
                },
                new Entities.Genre
                {
                    Name = "Science Fiction"
                },
                new Entities.Genre
                {
                    Name = "Romance"
                }
            );
    }
}
