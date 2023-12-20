using Npgsql;

namespace MoviesAPI;


/*Delete one actor*/ 
  public class ActorRepository
  {
    private readonly string _connectionString;

    public ActorRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void DeleteActor(int id)
    {
        using var dataSource = NpgsqlDataSource.Create(_connectionString);
        using var cmd = dataSource.CreateCommand("DELETE FROM actor WHERE actorid = @id");
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
    }
  }
