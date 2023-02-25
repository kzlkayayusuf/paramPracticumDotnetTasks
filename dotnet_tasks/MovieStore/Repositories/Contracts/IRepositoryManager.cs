namespace Repositories.Contracts;

// managerları kullanırken unitOfWork pattern i kullanılır.Birçok repo olabilir bunların hepsine manager üzerinden erişim verecez.
public interface IRepositoryManager
{
    IMovieRepository Movie { get; }
    Task SaveAsync();
}
