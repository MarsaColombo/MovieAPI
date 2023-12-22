namespace MoviesAPI
{
    /// <summary>
    /// Représente un acteur dans le système.
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// Obtient ou définit l'identifiant unique de l'acteur.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le prénom de l'acteur.
        /// </summary>
        public string? prenom { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de famille de l'acteur.
        /// </summary>
        public string? nom { get; set; }

        /// <summary>
        /// Obtient ou définit la date de naissance de l'acteur.
        /// </summary>
        public DateTime birthdate { get; set; }

        /// <summary>
        /// Obtient ou définit la date de création de l'entité acteur.
        /// </summary>
        public DateTime created_date { get; set; }

        /// <summary>
        /// Obtient ou définit la date de dernière modification de l'entité acteur.
        /// </summary>
        public DateTime modified_date { get; set; }

        /// <summary>
        /// Obtient ou définit l'âge de l'acteur. Peut être nul.
        /// </summary>
        public int? age { get; set; }
    }
}