namespace Entities.Exceptions;

// sealed keywordu kalıtım alınamaz olduğunu belirtmek için kullanılır.
public sealed class MovieNotFound : NotFound
{
    public MovieNotFound(int id) : base($"The Movie with id:{id} could not found")
    {
    }
}
