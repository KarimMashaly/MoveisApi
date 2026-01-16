namespace MoveisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService moviesService, IMapper mapper)
        {
            _moviesService = moviesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult>GetAllAsync()
        {
            var movies = await _moviesService.GetAll();

            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _moviesService.GetByID(id);

            var dto = _mapper.Map<MovieDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpGet("GetByGenreIdAsync/{genreId}")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _moviesService.GetAll(genreId);
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult>CreateAsync([FromForm]CreateMovieDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);
            var newMovie = await _moviesService.Add(movie, dto.Poster);
            var resultDto = _mapper.Map<MovieDetailsDto>(newMovie);

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm]UpdateMovieDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);
            var result = await _moviesService.Update(id, movie, dto.Poster);

            var resultDto = _mapper.Map<MovieDetailsDto>(result);

            return Ok(resultDto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _moviesService.Delete(id);

            return NoContent();
        }
    }
}
