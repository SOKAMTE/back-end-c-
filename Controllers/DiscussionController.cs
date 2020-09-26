using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using offreService.Repository;
using Microsoft.AspNetCore.Http;
using offreService.Models;
using System.Collections.Generic;
using System.Linq;

namespace offreService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController: ControllerBase
    {
        private readonly IDiscussionRepository discussionRepository;

        public DiscussionController(IDiscussionRepository discussionRepository)
        {
            this.discussionRepository = discussionRepository;
        }

        // GET: api/Discussion, Discussion
        /// <summary>
        /// Retourne la liste des Discussion
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>  
        /// <response code="200">Discussion selectionné</response>
        /// <response code="404">Discussion introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet]
        [ProducesResponseType(typeof(Discussion), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult> GetDiscussion()
        {
            try
            {
                return Ok(await discussionRepository.GetDiscussions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
					"Error retrieving data from the database");
            }
        }

        // GET: Discussion/Details/5, api/Discussion/5
        /// <summary>
        /// Retourne un Discussion specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id de la discussion à retourner</param>   
        /// <response code="200">Discussion selectionné</response>
        /// <response code="404">Discussion introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Discussion), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Discussion>> GetDiscussion(int id)
        {
            try
            {
                var result = await discussionRepository.GetDiscussion(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/Discussion, Discussion
        /// <summary>
        /// Créer un discussion à partir de ses attributs
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="discussion">objet discussion de la classe discussion du Discussion à créer</param>  
        /// <response code="200">discussion selectionné</response>
        /// <response code="404">discussion introuvable pour l'attribut specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPost]
        [ProducesResponseType(typeof(Discussion), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Discussion>> CreateDiscussion(Discussion discussion)
        {
            try
            {
                if (discussion == null)
                    return BadRequest();

                var createdDiscussion = await discussionRepository.AddDiscussion(discussion);

                return CreatedAtAction(nameof(GetDiscussion),
                    new { id = createdDiscussion.idDiscussion }, createdDiscussion);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new discussion record");
            }
        }

        // PUT: api/Discussion/5
        /// <summary>
        /// modifie un discussion specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du discussion à modifier</param>   
        /// <param name="discussion">classe d'entité du discussion à modifier</param>
        /// <response code="200">discussion selectionné</response>
        /// <response code="404">discussion introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Discussion), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Discussion>> UpdateDiscussion(int id, Discussion discussion)
        {
            try
            {
                if (id != discussion.idDiscussion)
                    return BadRequest("Discussion ID mismatch");

                var discussionToUpdate = await discussionRepository.GetDiscussion(id);

                if (discussionToUpdate == null)
                    return NotFound($"Discussion with Id = {id} not found");

                return await discussionRepository.UpdateDiscussion(discussion);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Discussion/5
        /// <summary>
        /// supprime un discussion specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du discussion à supprimer</param>   
        /// <response code="200">discussion selectionné</response>
        /// <response code="404">discussion introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Discussion), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Discussion>> DeleteDiscussion(int id)
        {
            try
            {
                var discussionToDelete = await discussionRepository.GetDiscussion(id);

                if (discussionToDelete == null)
                {
                    return NotFound($"Discussion with Id = {id} not found");
                }

                return await discussionRepository.DeleteDiscussion(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // GET: api/Discussion/value/5
        /// <summary>
        /// recherche un discussion specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="name">valeur de la discussion à rechercher</param>   
        /// <response code="200">discussion recherché</response>
        /// <response code="404">discussion introuvable pour la valeur specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("value/{search}")]
        [ProducesResponseType(typeof(Discussion), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<IEnumerable<Discussion>>> Search(string name)
        {
            try
            {
                var result = await discussionRepository.SearchDiscussion(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}