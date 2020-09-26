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
    public class ArticleTypeController: ControllerBase
    {
        private readonly IArticleTypeRepository articleTypeRepository;

        public ArticleTypeController(IArticleTypeRepository articleTypeRepository)
        {
            this.articleTypeRepository = articleTypeRepository;
        }

        // GET: api/ArticleType, Article
        /// <summary>
        /// Retourne la liste des ArticleType
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>  
        /// <response code="200">ArticleType selectionné</response>
        /// <response code="404">ArticleType introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet]
        [ProducesResponseType(typeof(ArticleType), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult> GetArticleType()
        {
            try
            {
                return Ok(await articleTypeRepository.GetArticleTypes());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
					"Error retrieving data from the database");
            }
        }

        // GET: ArticleType/Details/5, api/ArticleType/5
        /// <summary>
        /// Retourne un ArticleType specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du ArticleType a retourné</param>   
        /// <response code="200">ArticleType selectionné</response>
        /// <response code="404">ArticleType introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleType), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<ArticleType>> GetArticleType(int id)
        {
            try
            {
                var result = await articleTypeRepository.GetArticleType(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/ArticleType, ArticleType
        /// <summary>
        /// Créer un articleType à partir de ses attributs
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="articleType">objet articleType de la classe articleType du ArticleType à créer</param>  
        /// <response code="200">articleType selectionné</response>
        /// <response code="404">articleType introuvable pour l'attribut specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPost]
        [ProducesResponseType(typeof(ArticleType), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<ArticleType>> CreateArticleType(ArticleType articleType)
        {
            try
            {
                if (articleType == null)
                    return BadRequest();

                var createdArticleType = await articleTypeRepository.AddArticleType(articleType);

                return CreatedAtAction(nameof(GetArticleType),
                    new { id = createdArticleType.idArticleType }, createdArticleType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new articleType record");
            }
        }

        // PUT: api/ArticleType/5
        /// <summary>
        /// modifie un articleType specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du articleType à modifier</param>   
        /// <param name="articleType">classe d'entité du articleType à modifier</param>
        /// <response code="200">articleType selectionné</response>
        /// <response code="404">articleType introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ArticleType), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<ArticleType>> UpdateArticleType(int id, ArticleType articleType)
        {
            try
            {
                if (id != articleType.idArticleType)
                    return BadRequest("ArticleType ID mismatch");

                var articleTypeToUpdate = await articleTypeRepository.GetArticleType(id);

                if (articleTypeToUpdate == null)
                    return NotFound($"ArticleType with Id = {id} not found");

                return await articleTypeRepository.UpdateArticleType(articleType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/ArticleType/5
        /// <summary>
        /// supprime un articleType specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du articleType à supprimer</param>   
        /// <response code="200">articleType selectionné</response>
        /// <response code="404">articleType introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ArticleType), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<ArticleType>> DeleteArticleType(int id)
        {
            try
            {
                var articleTypeToDelete = await articleTypeRepository.GetArticleType(id);

                if (articleTypeToDelete == null)
                {
                    return NotFound($"ArticleType with Id = {id} not found");
                }

                return await articleTypeRepository.DeleteArticleType(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // GET: api/ArticleType/value/5
        /// <summary>
        /// recherche un articleType specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="name">valeur de l'articleType à rechercher</param>   
        /// <response code="200">articleType recherché</response>
        /// <response code="404">articleType introuvable pour la valeur specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("value/{search}")]
        [ProducesResponseType(typeof(ArticleType), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<IEnumerable<ArticleType>>> Search(string name)
        {
            try
            {
                var result = await articleTypeRepository.SearchArticleType(name);

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