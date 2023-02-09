using System;
using System.Linq;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    public int GenreId { get; set; }
    private readonly BookStoreDbContext context;

    public DeleteGenreCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("That Book Genre not found!");

        context.Genres.Remove(genre);
        context.SaveChanges();
    }
}
