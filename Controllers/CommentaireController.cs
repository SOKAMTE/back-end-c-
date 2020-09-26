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
    public class CommentaireController: ControllerBase
    {
        private readonly ICommentaireRepository commentaireRepository;

        public CommentaireController(ICommentaireRepository commentaireRepository)
        {
            this.commentaireRepository = commentaireRepository;
        }

        // GET: api/Commentaire, CommentaireRepository
        /// <summary>
        /// Retourne la liste des Commentaire
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>  
        /// <response code="200">Commentaire selectionné</response>
        /// <response code="404">Commentaire introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet]
        [ProducesResponseType(typeof(Commentaire), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult> GetCommentaire()
        {
            try
            {
                return Ok(await commentaireRepository.GetCommentaires());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
					"Error retrieving data from the database");
            }
        }

        // GET: Commentaire/Details/5, api/Commentaire/5
        /// <summary>
        /// Retourne un Commentaire specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du Commentaire a retourné</param>   
        /// <response code="200">Commentaire selectionné</response>
        /// <response code="404">Commentaire introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Commentaire), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Commentaire>> GetCommentaire(int id)
        {
            try
            {
                var result = await commentaireRepository.GetCommentaire(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/Commentaire, Commentaire
        /// <summary>
        /// Créer un commentaire à partir de ses attributs
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="commentaire">objet commentaire de la classe commentaire du Commentaire à créer</param>  
        /// <response code="200">commentaire selectionné</response>
        /// <response code="404">commentaire introuvable pour l'attribut specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPost]
        [ProducesResponseType(typeof(Commentaire), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Commentaire>> CreateCommentaire(Commentaire commentaire)
        {
            try
            {
                if (commentaire == null)
                    return BadRequest();

                var createdCommentaire = await commentaireRepository.AddCommentaire(commentaire);

                return CreatedAtAction(nameof(GetCommentaire),
                    new { id = createdCommentaire.idCommentaire }, createdCommentaire);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new commentaire record");
            }
        }

        // PUT: api/Commentaire/5
        /// <summary>
        /// modifie un commentaire specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du commentaire à modifier</param>   
        /// <param name="commentaire">classe d'entité du commentaire à modifier</param>
        /// <response code="200">commentaire selectionné</response>
        /// <response code="404">commentaire introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Commentaire), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Commentaire>> UpdateCommentaire(int id, Commentaire commentaire)
        {
            try
            {
                if (id != commentaire.idCommentaire)
                    return BadRequest("Commentaire ID mismatch");

                var commentaireToUpdate = await commentaireRepository.GetCommentaire(id);

                if (commentaireToUpdate == null)
                    return NotFound($"Commentaire with Id = {id} not found");

                return await commentaireRepository.UpdateCommentaire(commentaire);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Commentaire/5
        /// <summary>
        /// supprime un commentaire specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du commentaire à supprimer</param>   
        /// <response code="200">commentaire selectionné</response>
        /// <response code="404">commentaire introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Commentaire), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Commentaire>> DeleteCommentaire(int id)
        {
            try
            {
                var commentaireToDelete = await commentaireRepository.GetCommentaire(id);

                if (commentaireToDelete == null)
                {
                    return NotFound($"Commentaire with Id = {id} not found");
                }

                return await commentaireRepository.DeleteCommentaire(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // GET: api/Commentaire/value/5
        /// <summary>
        /// recherche un commentaire specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="name">valeur du commentaire à rechercher</param>   
        /// <response code="200">commentaire recherché</response>
        /// <response code="404">commentaire introuvable pour la valeur specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("value/{search}")]
        [ProducesResponseType(typeof(Commentaire), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<IEnumerable<Commentaire>>> Search(string name)
        {
            try
            {
                var result = await commentaireRepository.SearchCommentaire(name);

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