using MoviesAPI;
using Npgsql;

/// <summary>
/// Classe représentant le référentiel des acteurs pour l'API de films.
/// </summary>
public class ActorRepository
{
    private readonly string _connectionString;

    /// <summary>
    /// Initialise une nouvelle instance de la classe ActorRepository.
    /// </summary>
    /// <param name="connectionString">La chaîne de connexion à la base de données.</param>
    public ActorRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Ajoute un acteur à la base de données.
    /// </summary>
    /// <param name="actor">L'objet Actor représentant l'acteur à ajouter.</param>
    public void AddActor(Actor actor)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (var cmd = new NpgsqlCommand(
                       "INSERT INTO actor (firstname, lastname, birthdate, created_date, modified_date, age) " +
                       "VALUES (@prenom, @nom, @birthdate, @created_date, @modified_date, @age)", connection))
            {
                cmd.Parameters.AddWithValue("@prenom", actor.prenom);
                cmd.Parameters.AddWithValue("@nom", actor.nom);
                cmd.Parameters.AddWithValue("@birthdate", actor.birthdate);
                cmd.Parameters.AddWithValue("@created_date", actor.created_date);
                cmd.Parameters.AddWithValue("@modified_date", actor.modified_date);
                cmd.Parameters.AddWithValue("@age", actor.age);

                cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Supprime un acteur de la base de données en fonction de son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'acteur à supprimer.</param>
    public void DeleteActor(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();

            using (var cmd = new NpgsqlCommand("DELETE FROM actor WHERE actorid = @id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
