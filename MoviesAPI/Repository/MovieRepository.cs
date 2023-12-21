using JetBrains.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;

namespace MoviesAPI
{
    public class MovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = new();

            using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var cmd = dataSource.CreateCommand("SELECT * FROM movie");
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var movieToAdd = new Movie
                {
                    Id = reader["movieid"] is DBNull ? 0 : (int)reader["movieid"],
                    Title = reader["title"] is DBNull ? string.Empty : reader["title"].ToString(),
                    ReleaseYear = reader["release_year"] is DBNull ? 0 : (int)reader["release_year"],
                    CreateDate =
                        reader["created_date"] is DBNull ? DateTime.MinValue : (DateTime)reader["created_date"],
                    Duration = reader["duration"] is DBNull ? 0 : (int)reader["duration"]
                };
                movies.Add(movieToAdd);
            }

            return movies;
        }

        public Movie GetMovieById(int id)
        {
            Movie movie = new();

            using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var cmd = dataSource.CreateCommand("SELECT * FROM movie WHERE movieid = @id");
            cmd.Parameters.AddWithValue("id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                movie = new Movie
                {
                    Id = reader["movieid"] is DBNull ? 0 : (int)reader["movieid"],
                    Title = reader["title"] is DBNull ? string.Empty : reader["title"].ToString(),
                    ReleaseYear = reader["release_year"] is DBNull ? 0 : (int)reader["release_year"],
                    CreateDate =
                        reader["created_date"] is DBNull ? DateTime.MinValue : (DateTime)reader["created_date"],
                    Duration = reader["duration"] is DBNull ? 0 : (int)reader["duration"]
                };
            }

            return movie;
        }


        /*Ajouter un film selon juste sont titre et sa date de sortie*/
        public void AddMovie([FromBody] PostMovie postMovie)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var cmd = dataSource.CreateCommand(
                "INSERT INTO movie (title, release_year) VALUES (@titre, @dateSortie)");

            // Use AddWithValue for each parameter
            cmd.Parameters.AddWithValue("@titre", postMovie.titre ?? "No title");
            cmd.Parameters.AddWithValue("@dateSortie", postMovie.dateSortie);

            cmd.ExecuteNonQuery();
        }



        /*Modifier un film*/
        public void UpdateMovie(int id, Movie movie)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var cmd = dataSource.CreateCommand(
                "UPDATE movie SET title = @title, release_year = @release_year, created_date = @created_date, duration = @duration WHERE movieid = @id");
            cmd.Parameters.AddWithValue("id", movie.Id);
            cmd.Parameters.AddWithValue("title", movie.Title);
            cmd.Parameters.AddWithValue("release_year", movie.ReleaseYear);
            cmd.Parameters.AddWithValue("created_date", movie.CreateDate);
            cmd.Parameters.AddWithValue("duration", movie.Duration);
            cmd.ExecuteNonQuery();
            return;
        }
    }
}