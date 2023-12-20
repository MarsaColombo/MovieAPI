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
        /*Ajouter un film*/ 
        public void AddMovie(Movie movie)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var cmd = dataSource.CreateCommand("INSERT INTO movie (title, release_year, created_date, duration) VALUES (@title, @release_year, @created_date, @duration)");
            cmd.Parameters.AddWithValue("title", movie.Title);
            cmd.Parameters.AddWithValue("release_year", movie.ReleaseYear);
            cmd.Parameters.AddWithValue("created_date", movie.CreateDate);
            cmd.Parameters.AddWithValue("duration", movie.Duration);
            cmd.ExecuteNonQuery();
            return;
        }
        /*Modifier un film*/
        public void UpdateMovie(int id, Movie movie)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);
            using var cmd = dataSource.CreateCommand("UPDATE movie SET title = @title, release_year = @release_year, created_date = @created_date, duration = @duration WHERE movieid = @id");
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