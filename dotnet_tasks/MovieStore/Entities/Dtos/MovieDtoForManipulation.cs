using System.ComponentModel.DataAnnotations;
using Entities.Models.Enums;

namespace Entities.Dtos;

public abstract record MovieDtoForManipulation
{
    [Required(ErrorMessage = "Title is a required field.")]
    [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
    [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters")]
    public String Name { get; init; }

    [Required]
    public int ReleaseYear { get; init; }

    [Required]
    public Genre Genre { get; init; }

    [Required(ErrorMessage = "Price is a required field.")]
    [Range(50, 1000)]
    public decimal Price { get; init; }
}
