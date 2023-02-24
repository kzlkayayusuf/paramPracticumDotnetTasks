using Entities.Models.Enums;

namespace Entities.Dtos;

//[Serializable]
//public record MovieDto(int Id, String Name, int ReleaseYear, Genre Genre, decimal Price);

public record MovieDto
{
    public int Id { get; set; }
    public String Name { get; set; }
    public int ReleaseYear { get; set; }
    public Genre Genre { get; set; }
    public decimal Price { get; set; }
}