namespace MoveisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenresService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreService.GetAll();

            var resultDto = _mapper.Map<IEnumerable<GenreDto>>(genres);

            return Ok(resultDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(byte id)
        {
            var genre = await _genreService.GetById(id);
            var resultDto = _mapper.Map<GenreDto>(genre);

            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = _mapper.Map<Genre>(dto);

            var NewGenre = await _genreService.Add(genre);

            var resultDto = _mapper.Map<GenreDto>(NewGenre);

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CreateGenreDto dto)
        {
            var genre = _mapper.Map<Genre>(dto);
            var updated = await _genreService.Update(id, genre);

            var resultDto = _mapper.Map<GenreDto>(updated);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {

            await _genreService.Delete(id);

            return NoContent();
        }
    }
}
