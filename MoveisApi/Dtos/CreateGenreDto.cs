namespace MoveisApi.Dtos
{
    public class CreateGenreDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
