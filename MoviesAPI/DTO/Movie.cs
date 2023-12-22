namespace MoviesAPI
{
    /// <summary>
    /// Représente un film dans le système.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Obtient ou définit l'identifiant unique du film.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le titre du film.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Obtient ou définit l'année de sortie du film.
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Obtient ou définit la date de création de l'entité film.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Obtient ou définit la durée en minutes du film.
        /// </summary>
        public int Duration { get; set; }
    }
}