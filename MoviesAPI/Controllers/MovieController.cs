using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI
{
    /// <summary>
    /// Contrôleur pour la gestion des films dans le système.
    /// </summary>
    [ApiController]
    [Route("movies")]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepository _movieRepository;

        /// <summary>
        /// Initialise une nouvelle instance du contrôleur MovieController.
        /// </summary>
        /// <param name="movieRepository">Le référentiel des films utilisé par le contrôleur.</param>
        public MovieController(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Récupère tous les films du système.
        /// </summary>
        /// <returns>Code 200 (OK) avec la liste des films.</returns>
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = _movieRepository.GetAllMovies();
            return Ok(movies);
        }

        /// <summary>
        /// Récupère un film du système en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant unique du film à récupérer.</param>
        /// <returns>Code 200 (OK) avec les informations du film.</returns>
        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            return Ok(movie);
        }

        /// <summary>
        /// Ajoute un nouveau film dans le système.
        /// </summary>
        /// <param name="movie">Les informations du film à ajouter.</param>
        /// <returns>Code 200 (OK) si l'ajout est réussi.</returns>
        [HttpPost]
        public IActionResult AddMovie([FromBody] PostMovie movie)
        {
            _movieRepository.AddMovie(movie);
            return Ok();
        }

        /// <summary>
        /// Met à jour les informations d'un film dans le système.
        /// </summary>
        /// <param name="id">L'identifiant unique du film à mettre à jour.</param>
        /// <param name="movie">Les nouvelles informations du film.</param>
        /// <returns>Code 200 (OK) si la mise à jour est réussie.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] Movie movie)
        {
            _movieRepository.UpdateMovie(id, movie);
            return Ok();
        }
    }
}
