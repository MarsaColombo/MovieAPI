namespace MoviesAPI;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int ReleaseYear { get; set; }
    public DateTime CreateDate { get; set; }
    
    public int Duration { get; set; }
}