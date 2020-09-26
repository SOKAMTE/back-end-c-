using offreService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using offreService.Models;
using System.Linq;

namespace offreService.Repository
{
    public class CompteRepository: ICompteRepository
    {
        private readonly OffreServiceContext _context;

        public CompteRepository(OffreServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<Compte>> GetComptes()
        {
            return await _context.Comptes.ToListAsync();
        }

        public async Task<Compte> GetCompte(int idCompte)
        {
            return await _context.Comptes
                .FirstOrDefaultAsync(e => e.idCompte == idCompte);
        }

        public async Task<Compte> AddCompte(Compte compte)
        {
            var result = await _context.Comptes.AddAsync(compte);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Compte> UpdateCompte(Compte compte)
        {
            var result = await _context.Comptes
                .FirstOrDefaultAsync(e => e.idCompte == compte.idCompte);

            if (result != null)
            {
                result.idCompte = compte.idCompte;
                result.username = compte.username;
                result.password = compte.password;
                result.dateCreation = compte.dateCreation;
                result.dateConnexion = compte.dateConnexion;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Compte> DeleteCompte(int idCompte)
        {
            var result = await _context.Comptes
                .FirstOrDefaultAsync(e => e.idCompte == idCompte);
            if (result != null)
            {
                _context.Comptes.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Compte>> SearchCompte(string name)
        {
            IQueryable<Compte> query = _context.Comptes;
                
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.username.Contains(name)
                || e.password.Contains(name));
            }

            return await query.ToListAsync();
        }
    }
}