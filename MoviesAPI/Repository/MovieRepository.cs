using Npgsql;

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
                    Id = (int)reader["movieid"],
                    Title = reader["title"].ToString(),
                    ReleaseYear = (int)reader["release_year"],
                    CreateDate = (DateTime)reader["created_date"],
                    Duration = (int)reader["duration"]
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
                    Id = (int)reader["movieid"],
                    Title = reader["title"].ToString(),
                    ReleaseYear = (int)reader["release_year"],
                    CreateDate = (DateTime)reader["created_date"],
                    Duration = (int)reader["duration"]
                };
            }

            return movie;
        }
        

    }
}