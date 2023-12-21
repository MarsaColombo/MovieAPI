using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;

namespace MoviesAPI
{
    /// <summary>
    /// Représente un repository pour interagir avec la base de données des films.
    /// </summary>
    public class MovieRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initialise une nouvelle instance de la classe MovieRepository avec la chaîne de connexion à la base de données.
        /// </summary>
        /// <param name="connectionString">La chaîne de connexion à la base de données PostgreSQL.</param>
        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lit un objet Movie à partir du lecteur NpgsqlDataReader.
        /// </summary>
        /// <param name="reader">Le lecteur NpgsqlDataReader contenant les données du film.</param>
        /// <returns>Un objet Movie avec les données lues.</returns>
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

        /// <summary>
        /// Crée une instance NpgsqlCommand avec la requête SQL spécifiée et les paramètres fournis.
        /// </summary>
        /// <param name="sql">La requête SQL.</param>
        /// <param name="parameters">Les paramètres de la requête.</param>
        /// <returns>Une instance de NpgsqlCommand prête à être exécutée.</returns>
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

        /// <summary>
        /// Récupère la liste de tous les films depuis la base de données.
        /// </summary>
        /// <returns>Une liste d'objets Movie.</returns>
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

        /// <summary>
        /// Récupère un film spécifique à partir de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant du film.</param>
        /// <returns>Un objet Movie correspondant à l'identifiant fourni.</returns>
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

        /// <summary>
        /// Ajoute un nouveau film à la base de données.
        /// </summary>
        /// <param name="postMovie">Les détails du nouveau film à ajouter.</param>
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

        /// <summary>
        /// Met à jour les informations d'un film dans la base de données.
        /// </summary>
        /// <param name="id">L'identifiant du film à mettre à jour.</param>
        /// <param name="movie">Les nouvelles informations du film.</param>
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
