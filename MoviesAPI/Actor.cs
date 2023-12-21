namespace MoviesAPI;

public class Actor
{
    public int Id { get; set; }
    public string? prenom { get; set; }
    public string? nom { get; set; }
    public DateTime birthdate { get; set; }
    public DateTime created_date { get; set; }
    public DateTime modified_date { get; set; }
    public int? age { get; set; }
}