namespace MoveisApi.Dtos
{
    public class CreateMovieDto : BaseMovieDto
    {
        public IFormFile Poster { get; set; }
    }
}
