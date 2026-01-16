using MoveisApi.Middlewares;
using MoveisApi.Services;
using Scalar.AspNetCore;

namespace MoveisApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
            #endregion

            #region Enable CORS
            builder.Services.AddCors();//to enable CORS
            #endregion

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<IGenresService, GenresService>();
            builder.Services.AddScoped<IMoviesService, MoviesService>();

            //builder.Services.AddAutoMapper(typeof(Program));
            #region Adding AutoMapper
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddMaps(typeof(Program).Assembly);
            });
            #endregion

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseExceptionHandler();
            app.UseHttpsRedirection();

            #region Enable CORS
            app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());//to enable CORS
            #endregion

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
