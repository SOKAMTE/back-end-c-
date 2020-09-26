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
    public class AdminController: ControllerBase
    {
        private readonly IAdminRepository adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        // GET: api/Admin, Admin
        /// <summary>
        /// Retourne la liste des Admins
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>  
        /// <response code="200">Admin selectionné</response>
        /// <response code="404">Admin introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet]
        [ProducesResponseType(typeof(Admin), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult> GetAdmin()
        {
            try
            {
                return Ok(await adminRepository.GetAdmins());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
					"Error retrieving data from the database");
            }
        }

        // GET: Admin/Details/5, api/Admin/5
        /// <summary>
        /// Retourne un Admin specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du Admin a retourné</param>   
        /// <response code="200">Admin selectionné</response>
        /// <response code="404">Admin introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Admin), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            try
            {
                var result = await adminRepository.GetAdmin(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST: api/Admin, Admin
        /// <summary>
        /// Créer un admin à partir de ses attributs
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="admin">objet admin de la classe admin du Admin à créer</param>  
        /// <response code="200">admin selectionné</response>
        /// <response code="404">admin introuvable pour l'attribut specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPost]
        [ProducesResponseType(typeof(Admin), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Admin>> CreateAdmin(Admin admin)
        {
            try
            {
                if (admin == null) {
                    return BadRequest();
                }

                // Add custom model validation error
                var adm = adminRepository.GetAdminByMail(admin.mail);

                if(adm != null)
                {
                    ModelState.AddModelError("email", "Admin email already in use");
                    return BadRequest(ModelState);
                }

                var createdAdmin = await adminRepository.AddAdmin(admin);

                return CreatedAtAction(nameof(GetAdmin),
                    new { id = createdAdmin.idAdmin }, createdAdmin);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new admin record");
            }
        }

        // PUT: api/Admin/5
        /// <summary>
        /// modifie un admin specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du admin à modifier</param>   
        /// <param name="admin">classe d'entité du admin à modifier</param>
        /// <response code="200">admin selectionné</response>
        /// <response code="404">admin introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Admin), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Admin>> UpdateAdmin(int id, Admin admin)
        {
            try
            {
                if (id != admin.idAdmin)
                    return BadRequest("Admin ID mismatch");

                var adminToUpdate = await adminRepository.GetAdmin(id);

                if (adminToUpdate == null)
                    return NotFound($"Admin with Id = {id} not found");

                return await adminRepository.UpdateAdmin(admin);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE: api/Admin/5
        /// <summary>
        /// supprime un admin specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="id">id du admin à supprimer</param>   
        /// <response code="200">admin selectionné</response>
        /// <response code="404">admin introuvable pour l'id specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Admin), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<Admin>> DeleteAdmin(int id)
        {
            try
            {
                var adminToDelete = await adminRepository.GetAdmin(id);

                if (adminToDelete == null)
                {
                    return NotFound($"Admin with Id = {id} not found");
                }

                return await adminRepository.DeleteAdmin(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // GET: api/Admin/value/5
        /// <summary>
        /// recherche un admin specifique à partir de son id
        /// </summary>
        /// <remarks>Je manque d'imagination</remarks>
        /// <param name="name">valeur de l'admin à rechercher</param>   
        /// <response code="200">admin recherché</response>
        /// <response code="404">admin introuvable pour la valeur specifié</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        [HttpGet("value/{search}")]
        [ProducesResponseType(typeof(Admin), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<ActionResult<IEnumerable<Admin>>> Search(string name)
        {
            try
            {
                var result = await adminRepository.SearchAdmin(name);

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