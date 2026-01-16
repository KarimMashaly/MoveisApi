namespace MoveisApi.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre>Add(Genre genre);
        Task<Genre>Update(byte id, Genre genre);
        Task<bool> Delete(byte id);
        Task<bool>IsValidGenre (byte id);
    }
}
