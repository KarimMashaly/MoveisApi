namespace MoveisApi.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        Task<Movie> GetByID(int id);
        Task<Movie> Add(Movie movie, IFormFile Poster);
        Task<Movie> Update(int id, Movie movie, IFormFile? Poster);
        Task Delete(int id);
    }
}