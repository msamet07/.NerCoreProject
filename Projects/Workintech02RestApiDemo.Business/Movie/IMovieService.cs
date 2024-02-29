namespace Workintech02RestApiDemo.Business.Movie
{
    public interface IMovieService:IBaseService
    {
        Task<Domain.Entities.Movie> CreateMovieAsync(Domain.Entities.Movie movie);
        Task DeleteMovieAsync(int id);
        Task<Domain.Entities.Movie> GetMovieAsync(int id);
        Task<List<Domain.Entities.Movie>> GetMoviesAsync();
        Task UpdateMovieAsync(Domain.Entities.Movie movie);
    }
}