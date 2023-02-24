namespace Entities.Exceptions;

public abstract class NotFoundException : Exception
{
    // abstract class olduğu için sadece kalıtım alan yerde çalışması için protected yaptık.
    protected NotFoundException(string message) : base(message)
    {
    }
}
