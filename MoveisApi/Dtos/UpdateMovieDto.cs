namespace MoveisApi.Dtos
{
    public class UpdateMovieDto : BaseMovieDto
    {
        public IFormFile? Poster { get; set; }
    }
}
