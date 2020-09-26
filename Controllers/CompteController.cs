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
    public class CompteController: ControllerBase
    {
        private readonly ICompteRepository compteRepository;

        public CompteController(ICompteRepository compteRepository)
        {
            this.compteRepository = compteRepository;
        }

        // GET: api/Compte, Compte
        /// <summary>
        /// Retourne la liste des Compte
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>  
        /// <response code="200">Compte selectionné</response>
        /// <response code="404">Compte introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet]
        [ProducesResponseType(typeof(Compte), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult> GetCompte()
        {
            try
            {
                return Ok(await compteRepository.GetComptes());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
					"Error retrieving data from the database");
            }
        }

        // GET: Compte/Details/5, api/Compte/5
        /// <summary>
        /// Retourne un Compte specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du Compte a retourné</param>   
        /// <response code="200">Compte selectionné</response>
        /// <response code="404">Compte introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Compte), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Compte>> GetCompte(int id)
        {
            try
            {
                var result = await compteRepository.GetCompte(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/Compte, Compte
        /// <summary>
        /// Créer un compte à partir de ses attributs
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="compte">objet compte de la classe compte du Compte à créer</param>  
        /// <response code="200">compte selectionné</response>
        /// <response code="404">compte introuvable pour l'attribut specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPost]
        [ProducesResponseType(typeof(Compte), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Compte>> CreateCompte(Compte compte)
        {
            try
            {
                if (compte == null)
                    return BadRequest();

                var createdCompte = await compteRepository.AddCompte(compte);

                return CreatedAtAction(nameof(GetCompte),
                    new { id = createdCompte.idCompte }, createdCompte);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new compte record");
            }
        }

        // PUT: api/Compte/5
        /// <summary>
        /// modifie un compte specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du compte à modifier</param>   
        /// <param name="compte">classe d'entité du compte à modifier</param>
        /// <response code="200">compte selectionné</response>
        /// <response code="404">compte introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Compte), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Compte>> UpdateCompte(int id, Compte compte)
        {
            try
            {
                if (id != compte.idCompte)
                    return BadRequest("Compte ID mismatch");

                var compteToUpdate = await compteRepository.GetCompte(id);

                if (compteToUpdate == null)
                    return NotFound($"Compte with Id = {id} not found");

                return await compteRepository.UpdateCompte(compte);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Compte/5
        /// <summary>
        /// supprime un compte specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du compte à supprimer</param>   
        /// <response code="200">compte selectionné</response>
        /// <response code="404">compte introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Compte), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Compte>> DeleteCompte(int id)
        {
            try
            {
                var compteToDelete = await compteRepository.GetCompte(id);

                if (compteToDelete == null)
                {
                    return NotFound($"Compte with Id = {id} not found");
                }

                return await compteRepository.DeleteCompte(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // GET: api/Compte/value/5
        /// <summary>
        /// recherche un compte specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="name">valeur du compte à rechercher</param>   
        /// <response code="200">compte recherché</response>
        /// <response code="404">compte introuvable pour la valeur specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("value/{search}")]
        [ProducesResponseType(typeof(Compte), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<IEnumerable<Compte>>> Search(string name)
        {
            try
            {
                var result = await compteRepository.SearchCompte(name);

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