namespace MoveisApi.Dtos
{
    public class BaseMovieDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public double Rate { get; set; }

        [MaxLength(2500)]
        public string Storeline { get; set; } = null!;
        public byte GenreId { get; set; }
    }
}
