namespace Entities.Exceptions;

public abstract class NotFound : Exception
{
    // abstract class olduğu için sadece kalıtım alan yerde çalışması için protected yaptık.
    protected NotFound(string message) : base(message)
    {
    }
}
