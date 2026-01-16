namespace MoveisApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<CreateMovieDto, Movie>().ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<UpdateMovieDto, Movie>().ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<CreateGenreDto, Genre>();
        }
    }
}
