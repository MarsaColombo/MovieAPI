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
    public void AddActor(Actor actor)
    {
        using var dataSource = NpgsqlDataSource.Create(_connectionString);
        using var cmd = dataSource.CreateCommand("INSERT INTO actor (firstname, lastname, birthdate, created_date, modified_date, age) VALUES (@prenom, @nom, @birthdate, @created_date, @modified_date, @age)");
        cmd.Parameters.AddWithValue("prenom", actor.prenom);
        cmd.Parameters.AddWithValue("nom", actor.nom);
        cmd.Parameters.AddWithValue("birthdate", actor.birthdate);
        cmd.Parameters.AddWithValue("created_date", actor.created_date);
        cmd.Parameters.AddWithValue("modified_date", actor.modified_date);
        cmd.Parameters.AddWithValue("age", actor.age);
        cmd.ExecuteNonQuery();
    }

    /*Delete one actor*/
    public void DeleteActor(int id)
    {
        using var dataSource = NpgsqlDataSource.Create(_connectionString);
        using var cmd = dataSource.CreateCommand("DELETE FROM actor WHERE actorid = @id");
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
    }
}