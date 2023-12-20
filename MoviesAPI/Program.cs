using MoviesAPI;
using Npgsql;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace MovieMinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*Add Cors to access every data outside */
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            /*    Add cors for everything*/


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseCors();

            /*Constant for connection string*/
            var keyConnectionString =
                @"Host=localhost;Port=5436;Username=postgres;Password=PirateBiaou@123;Database=movie_db;Pooling=true;";


            // Use MapGet to specify the endpoint and handler
            var movieRepository =
                new MovieRepository(keyConnectionString);
            app.MapGet("/movies", () =>
            {
                var movies = movieRepository.GetAllMovies();
                return Results.Json(movies);
            });

            var movieRepository2 =
                new MovieRepository(keyConnectionString);
            app.MapGet("/movies/{id}", (int id) =>
            {
                var movie = movieRepository2.GetMovieById(id);
                return Results.Json(movie);
            });
            
            var movieRepository3 =
                new MovieRepository(keyConnectionString);
            app.MapPost("/movies", (Movie movie) =>
            {
                movieRepository3.AddMovie(movie);
                return Results.Json(movie);
            });
            
            var movieRepository4 =
                new MovieRepository(keyConnectionString);
            app.MapPut("/movies/{id}", (int id, Movie movie) =>
            {
                movieRepository4.UpdateMovie(id, movie);
                return Results.Json(movie);
            });
            
            var ActorRepository =
                new ActorRepository(keyConnectionString);
            app.MapDelete("/actors/{id}", (int id) =>
            {
                ActorRepository.DeleteActor(id);
                
                return Results.Ok();
            });

            app.Run();
        }
    }
}