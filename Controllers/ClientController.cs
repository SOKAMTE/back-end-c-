
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using offreService.Data;
using offreService.Models;

namespace offreService.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly OffreServiceContext _context;

        public ClientController(OffreServiceContext context)
        {
            _context = context;    
        }

        // GET: api/Client, Client
        /// <summary>
        /// Retourne la liste des clients
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>  
        /// <response code="200">client selectionné</response>
        /// <response code="404">client introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClient()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: Client/Details/5, api/Client/5
        /// <summary>
        /// Retourne un client specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du client a retourné</param>   
        /// <response code="200">client selectionné</response>
        /// <response code="404">client introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Client>> Details(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }*/

            var entity = await _context.Clients.SingleOrDefaultAsync(m => m.idClient == id);
            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // POST: api/Client, Client
        /// <summary>
        /// Créer un client à partir de ses attributs
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="entity">objet entity de la classe client du Client à créer</param>  
        /// <response code="200">client selectionné</response>
        /// <response code="404">client introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPost]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Client>> Create(Client entity)
        {
            _context.Clients.Add(entity);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(Create), new { id = entity.idClient }, entity);
        }

        // PUT: api/Client/5
        /// <summary>
        /// modifie un client specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du client à modifier</param>   
        /// <param name="entity">classe d'entité du client à modifier</param>
        /// <response code="200">client selectionné</response>
        /// <response code="404">client introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> PutTodoItem(long id, Client entity)
        {
            if (id != entity.idClient)
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Client/5
        /// <summary>
        /// supprime un client specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du client à supprimer</param>   
        /// <response code="200">client selectionné</response>
        /// <response code="404">client introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            var entity = await _context.Clients.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        private bool ClientExists(long id) =>
         _context.Clients.Any(e => e.idClient == id);


    }
}
    