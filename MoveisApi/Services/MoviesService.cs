using Microsoft.Extensions.Configuration;

namespace MoveisApi.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly AppDbContext _context;
        private readonly IGenresService _genresService;
        private readonly IConfiguration _configuration;
        private long _maxAllowedSize => _configuration.GetValue<long>("FileSettings:MaxAllowedSizeInBytes");
        private List<string> _allowedExtensions => _configuration.GetSection("FileSettings:AllowedExtensions").Get<List<string>>()!;
        public MoviesService(AppDbContext context, IGenresService genresService, IConfiguration configuration)
        {
            _context = context;
            _genresService = genresService;
            _configuration = configuration;
        }

        public async Task Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                throw new BusinessException("Movie not found", StatusCodes.Status404NotFound);

            _context.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await _context.Movies
                .Where(m => m.GenreId == genreId || genreId == 0)
                .OrderByDescending(g => g.Rate)
                .Include(g => g.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetByID(int id)
        {
            var movie =  await _context.Movies.Include(m => m.Genre)
                .SingleOrDefaultAsync(m=> m.Id == id);

            if(movie == null)
                throw new BusinessException("Movie not found", StatusCodes.Status404NotFound);

            return movie;
        }
        public async Task<Movie> Add(Movie movie, IFormFile Poster)
        {
            await ValidateGenre(movie.GenreId);

            movie.Poster = await ProcessPoster(Poster);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            //to load navigaiton property
            await _context.Entry(movie).Reference(m => m.Genre).LoadAsync();

            return movie;
        }

        public async Task<Movie> Update(int id, Movie movie, IFormFile? Poster)
        {
            var updatedMovie = await _context.Movies.Include(m=> m.Genre).SingleOrDefaultAsync(m=>m.Id == id);
            if (updatedMovie == null)
                throw new BusinessException($"No movie was found wiht ID {id}", StatusCodes.Status404NotFound);

            await ValidateGenre(movie.GenreId);

            if (Poster != null)
            {
                updatedMovie.Poster = await ProcessPoster(Poster);
            }

            //we did mapping manually because EF Core had tracked this object
            updatedMovie.Title = movie.Title;
            updatedMovie.Year = movie.Year;
            updatedMovie.Rate = movie.Rate;
            updatedMovie.Storeline = movie.Storeline;
            updatedMovie.GenreId = movie.GenreId;

            await _context.SaveChangesAsync();

            return updatedMovie;
        }

        private async Task ValidateGenre(byte genreId)
        {
            bool isValidGenre = await _genresService.IsValidGenre(genreId);
            if (!isValidGenre)
                throw new BusinessException("Invalid genre ID!", StatusCodes.Status400BadRequest);
        }

        private async Task<byte[]> ProcessPoster(IFormFile Poster)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(Poster.FileName).ToLower()))
                throw new BusinessException("Invalid extension!, Only .png and .jpg images are allowed", StatusCodes.Status400BadRequest);

            if (Poster.Length > _maxAllowedSize)
                throw new BusinessException("File size exceeds limit!, Max allowd size for poster is 1MB!", StatusCodes.Status400BadRequest);

            using var dataStream = new MemoryStream();
            await Poster.CopyToAsync(dataStream);

            return dataStream.ToArray();
        }
    }
}
