namespace Entities.Exceptions;

// sealed keywordu kalıtım alınamaz olduğunu belirtmek için kullanılır.
public sealed class MovieNotFoundException : NotFoundException
{
    public MovieNotFoundException(int id) : base($"The Movie with id:{id} could not found")
    {
    }
}
