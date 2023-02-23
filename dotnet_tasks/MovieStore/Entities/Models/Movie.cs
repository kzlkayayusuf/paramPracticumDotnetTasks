using Entities.Models.Enums;

namespace Entities.Models;

public class Movie
{
    public int Id { get; set; }
    public String Name { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public Genre Genre { get; set; }
    public decimal Price { get; set; }
}
