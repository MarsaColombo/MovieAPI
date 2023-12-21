using System;
using System.Collections.Generic;
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

        private Movie ReadMovieFromReader(NpgsqlDataReader reader)
        {
            return new Movie
            {
                Id = reader["movieid"] is DBNull ? 0 : (int)reader["movieid"],
                Title = reader["title"] is DBNull ? string.Empty : reader["title"].ToString(),
                ReleaseYear = reader["release_year"] is DBNull ? 0 : (int)reader["release_year"],
                CreateDate = reader["created_date"] is DBNull ? DateTime.MinValue : (DateTime)reader["created_date"],
                Duration = reader["duration"] is DBNull ? 0 : (int)reader["duration"]
            };
        }

        private NpgsqlCommand CreateCommand(string sql, Dictionary<string, object> parameters = null)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);
            var cmd = dataSource.CreateCommand(sql);

            if (parameters != null)
            {
                foreach (var (key, value) in parameters)
                {
                    cmd.Parameters.AddWithValue(key, value);
                }
            }

            return cmd;
        }

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();

            using (var dataSource = NpgsqlDataSource.Create(_connectionString))
            {
                using var cmd = dataSource.CreateCommand("SELECT * FROM movie");
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    movies.Add(ReadMovieFromReader(reader));
                }
            }

            return movies;
        }

        public Movie GetMovieById(int id)
        {
            var movie = new Movie();

            using (var dataSource = NpgsqlDataSource.Create(_connectionString))
            {
                using var cmd = dataSource.CreateCommand("SELECT * FROM movie WHERE movieid = @id");
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    movie = ReadMovieFromReader(reader);
                }
            }

            return movie;
        }

        public void AddMovie([FromBody] PostMovie postMovie)
        {
            var sql = "INSERT INTO movie (title, release_year) VALUES (@titre, @dateSortie)";
            var parameters = new Dictionary<string, object>
            {
                { "@titre", postMovie.titre ?? "No title" },
                { "@dateSortie", postMovie.dateSortie }
            };

            using (var dataSource = NpgsqlDataSource.Create(_connectionString))
            using (var connection = dataSource.CreateConnection())
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                foreach (var (key, value) in parameters)
                {
                    cmd.Parameters.AddWithValue(key, value);
                }

                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateMovie(int id, Movie movie)
        {
            var sql =
                "UPDATE movie SET title = @title, release_year = @release_year, created_date = @created_date, duration = @duration WHERE movieid = @id";

            var parameters = new Dictionary<string, object>
            {
                { "@id", movie.Id },
                { "@title", movie.Title },
                { "@release_year", movie.ReleaseYear },
                { "@created_date", movie.CreateDate },
                { "@duration", movie.Duration }
            };

            using (var dataSource = NpgsqlDataSource.Create(_connectionString))
            using (var connection = dataSource.CreateConnection())
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                foreach (var (key, value) in parameters)
                {
                    cmd.Parameters.AddWithValue(key, value);
                }


                cmd.ExecuteNonQuery();
            }
        }
    }
}