using offreService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using offreService.Models;
using System.Linq;

namespace offreService.Repository
{
    public class AdminRepository: IAdminRepository
    {
        private readonly OffreServiceContext _context;

        public AdminRepository(OffreServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetAdmin(int idAdmin)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(e => e.idAdmin == idAdmin);
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            var result = await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            var result = await _context.Admins
                .FirstOrDefaultAsync(e => e.idAdmin == admin.idAdmin);

            if (result != null)
            {
                result.idAdmin = admin.idAdmin;
                result.username = admin.username;
                result.password = admin.password;
                result.mail = admin.mail;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Admin> DeleteAdmin(int idAdmin)
        {
            var result = await _context.Admins
                .FirstOrDefaultAsync(e => e.idAdmin == idAdmin);
            if (result != null)
            {
                _context.Admins.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Admin>> SearchAdmin(string name)
        {
            IQueryable<Admin> query = _context.Admins;
                
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.username.Contains(name)
                            || e.password.Contains(name) || e.mail.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<Admin> GetAdminByEmail(string email)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(e => e.mail == email);
        }
    }
}