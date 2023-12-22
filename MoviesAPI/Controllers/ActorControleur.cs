using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI
{
    /// <summary>
    /// Contrôleur pour la gestion des acteurs dans le système.
    /// </summary>
    [ApiController]
    [Route("actors")]
    public class ActorController : ControllerBase
    {
        private readonly ActorRepository _actorRepository;

        /// <summary>
        /// Initialise une nouvelle instance du contrôleur ActorController.
        /// </summary>
        /// <param name="actorRepository">Le référentiel des acteurs utilisé par le contrôleur.</param>
        public ActorController(ActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        /// <summary>
        /// Ajoute un nouvel acteur dans le système.
        /// </summary>
        /// <param name="actor">Les informations de l'acteur à ajouter.</param>
        /// <returns>Code 200 (OK) si l'ajout est réussi.</returns>
        [HttpPost]
        public IActionResult AddActor([FromBody] Actor actor)
        {
            _actorRepository.AddActor(actor);
            return Ok();
        }
        
        /// <summary>
        /// Supprime un acteur du système en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'acteur à supprimer.</param>
        /// <returns>Code 200 (OK) si la suppression est réussie.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            _actorRepository.DeleteActor(id);
            return Ok();
        }
    }
}