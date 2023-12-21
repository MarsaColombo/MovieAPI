using Microsoft.AspNetCore.Mvc;
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
    /*Add Actor*/
    public void AddActor([FromBody] Actor actor)
    {
        using var dataSource = NpgsqlDataSource.Create(_connectionString);
        using var cmd = dataSource.CreateCommand(
            "INSERT INTO actor (firstname, lastname, birthdate, created_date, modified_date) VALUES (@firstname, @lastname, @birth_year, @created_date, @last_update)");

        // Use AddWithValue for each parameter and handle possible null values
        cmd.Parameters.AddWithValue("@first_name", actor.firstname ?? "No first name");
        cmd.Parameters.AddWithValue("@last_name", actor.lastname ?? "No last name");
        cmd.Parameters.AddWithValue("@birth_year", actor.birthdate);
        cmd.Parameters.AddWithValue("@created_date", actor.created_date);
        cmd.Parameters.AddWithValue("@last_update", actor.modified_date);

        cmd.ExecuteNonQuery();
    }


    public void DeleteActor(int id)
    {
        using var dataSource = NpgsqlDataSource.Create(_connectionString);
        using var cmd = dataSource.CreateCommand("DELETE FROM actor WHERE actorid = @id");
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
    }
  }
