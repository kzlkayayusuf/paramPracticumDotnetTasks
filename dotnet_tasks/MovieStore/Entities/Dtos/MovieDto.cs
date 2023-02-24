using Entities.Models.Enums;

namespace Entities.Dtos;

//[Serializable]
//public record MovieDto(int Id, String Name, int ReleaseYear, Genre Genre, decimal Price);

public record MovieDto
{
    public int Id { get; init; }
    public String Name { get; init; }
    public int ReleaseYear { get; init; }
    public Genre Genre { get; init; }
    public decimal Price { get; init; }
}