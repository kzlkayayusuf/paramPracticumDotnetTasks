using Entities.Models;
using Entities.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config;

public class MovieConfig : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasData(
            new Movie { Id = 1, Name = "Gladiator", ReleaseYear = 2000, Genre = Genre.Action, Price = 88 },
            new Movie { Id = 2, Name = "Kingdom of Heaven", ReleaseYear = 2005, Genre = Genre.Adventure, Price = 98 },
            new Movie { Id = 3, Name = "Pokemon Detective Pikachu", ReleaseYear = 2019, Genre = Genre.Anime, Price = 67 },
            new Movie { Id = 4, Name = "Geniş Aile", ReleaseYear = 2015, Genre = Genre.Comedy, Price = 102 },
            new Movie { Id = 5, Name = "Saw", ReleaseYear = 2004, Genre = Genre.Horror, Price = 105 },
            new Movie { Id = 6, Name = "Avatar", ReleaseYear = 2009, Genre = Genre.ScienceFiction, Price = 107 }
        );
    }
}
