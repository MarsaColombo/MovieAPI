namespace MoviesAPI
{
    /// <summary>
    /// Représente les données requises pour créer un nouveau film dans le système.
    /// </summary>
    public class PostMovie
    {
        /// <summary>
        /// Obtient ou définit le titre du film. Non nul.
        /// </summary>
        public string titre { get; set; } = null!;

        /// <summary>
        /// Obtient ou définit l'année de sortie du film.
        /// </summary>
        public int dateSortie { get; set; }
    }
}