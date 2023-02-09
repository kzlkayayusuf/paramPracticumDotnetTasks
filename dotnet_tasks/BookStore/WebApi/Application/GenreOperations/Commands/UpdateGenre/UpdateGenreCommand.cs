using System;
using System.Linq;


namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }
    private readonly BookStoreDbContext context;

    public UpdateGenreCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("No genre found to be updated");
        if (context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            throw new InvalidOperationException("a book genre with the same name already exists");

        genre.Name = Model.Name.Trim() == default ? Model.Name : genre.Name;
        genre.IsActive = Model.IsActive;
        context.SaveChanges();
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
