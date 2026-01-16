
using MoveisApi.Exceptions;

namespace MoveisApi.Services
{
    public class GenresService : IGenresService
    {
        private readonly AppDbContext _context;

        public GenresService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Add(Genre genre)
        {

            bool exists = await _context.Genres.AnyAsync(g => g.Name.ToLower() == genre.Name.ToLower().Trim());

            if (exists)
                throw new BusinessException("Genre already exists", StatusCodes.Status409Conflict);

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<bool> Delete(byte id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                throw new BusinessException("Genre not found", StatusCodes.Status404NotFound);

            _context.Genres.Remove(genre);
            await _context .SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.AsNoTracking().OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genre> GetById(byte id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                throw new BusinessException("Genre not found", StatusCodes.Status404NotFound);

            return genre;
        }

        public async Task<bool> IsValidGenre(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<Genre> Update(byte id,Genre genre)
        {
            var existing = await _context.Genres.FindAsync(id);

            if (existing == null)
                throw new BusinessException("Genre not found", StatusCodes.Status404NotFound);

            var isNameUsed = await _context.Genres
                .AnyAsync(g => g.Id != id && g.Name.ToLower() == genre.Name.ToLower().Trim());

            if (isNameUsed)
                throw new BusinessException("Genre already exists", StatusCodes.Status409Conflict);

            existing.Name = genre.Name.Trim();
            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
