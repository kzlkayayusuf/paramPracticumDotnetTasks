namespace Services.Contracts;

public interface IServiceManager
{
    IMovieService MovieService { get; }
    IAuthenticationService AuthenticationService { get; }
}
