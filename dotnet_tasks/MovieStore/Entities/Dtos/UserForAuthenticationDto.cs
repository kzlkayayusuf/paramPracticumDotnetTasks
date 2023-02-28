using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Username is required!")]
    public string? Username { get; init; }
    [Required(ErrorMessage = "Password is required!")]
    public string? Password { get; init; }
}
