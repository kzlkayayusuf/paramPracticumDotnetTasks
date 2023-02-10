using System;
using System.Linq;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    private readonly IBookStoreDbContext context;

    public CreateGenreCommand(IBookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if (genre is not null)
            throw new InvalidOperationException("That Book Genre is exists");

        genre = new Entities.Genre();
        genre.Name = Model.Name;
        context.Genres.Add(genre);
        context.SaveChanges();
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
