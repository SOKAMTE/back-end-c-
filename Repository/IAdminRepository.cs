using offreService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace offreService.Repository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin> GetAdmin(int idAdmin);
        Task<Admin> AddAdmin(Admin admin);
        Task<Admin> UpdateAdmin(Admin admin);
        Task<Admin> DeleteAdmin(int idAdmin);
        Task<IEnumerable<Admin>> SearchAdmin(string name);
        Task<Admin> GetAdminByMail(string email);
    } 
}