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
    public class ArticleController: ControllerBase
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        // GET: api/Article, Article
        /// <summary>
        /// Retourne la liste des Article
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>  
        /// <response code="200">Article selectionné</response>
        /// <response code="404">Article introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet]
        [ProducesResponseType(typeof(Article), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult> GetArticle()
        {
            try
            {
                return Ok(await articleRepository.GetArticles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
					"Error retrieving data from the database");
            }
        }

        // GET: Article/Details/5, api/Article/5
        /// <summary>
        /// Retourne un Article specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du Article a retourné</param>   
        /// <response code="200">Article selectionné</response>
        /// <response code="404">Article introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Article), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            try
            {
                var result = await articleRepository.GetArticle(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/Article, Article
        /// <summary>
        /// Créer un article à partir de ses attributs
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="article">objet article de la classe article du Article à créer</param>  
        /// <response code="200">article selectionné</response>
        /// <response code="404">article introuvable pour l'attribut specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPost]
        [ProducesResponseType(typeof(Article), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            try
            {
                if (article == null)
                    return BadRequest();

                var createdArticle = await articleRepository.AddArticle(article);

                return CreatedAtAction(nameof(GetArticle),
                    new { id = createdArticle.IdArticle }, createdArticle);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new article record");
            }
        }

        // PUT: api/Article/5
        /// <summary>
        /// modifie un article specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du article à modifier</param>   
        /// <param name="article">classe d'entité du article à modifier</param>
        /// <response code="200">article selectionné</response>
        /// <response code="404">article introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Article), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Article>> UpdateArticle(int id, Article article)
        {
            try
            {
                if (id != article.IdArticle)
                    return BadRequest("Article ID mismatch");

                var articleToUpdate = await articleRepository.GetArticle(id);

                if (articleToUpdate == null)
                    return NotFound($"Article with Id = {id} not found");

                return await articleRepository.UpdateArticle(article);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Article/5
        /// <summary>
        /// supprime un article specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du article à supprimer</param>   
        /// <response code="200">article selectionné</response>
        /// <response code="404">article introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Article), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            try
            {
                var articleToDelete = await articleRepository.GetArticle(id);

                if (articleToDelete == null)
                {
                    return NotFound($"Article with Id = {id} not found");
                }

                return await articleRepository.DeleteArticle(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // GET: api/Article/value/5
        /// <summary>
        /// recherche un article specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="name">valeur de l'article à rechercher</param>   
        /// <response code="200">article recherché</response>
        /// <response code="404">article introuvable pour la valeur specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("value/{search}")]
        [ProducesResponseType(typeof(Article), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<IEnumerable<Article>>> Search(string name)
        {
            try
            {
                var result = await articleRepository.SearchArticle(name);

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