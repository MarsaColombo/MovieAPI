using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI
{
    [ApiController]
    [Route("actors")]
    public class ActorController : ControllerBase
    {
        private readonly ActorRepository _actorRepository;

        public ActorController(ActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpPost]
        public IActionResult AddActor([FromBody] Actor actor)
        {
            _actorRepository.AddActor(actor);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            _actorRepository.DeleteActor(id);
            return Ok();
        }
    }
}