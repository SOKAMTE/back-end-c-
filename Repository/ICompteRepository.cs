using offreService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace offreService.Repository
{
    public interface ICompteRepository
    {
        Task<IEnumerable<Compte>> GetComptes();
        Task<Compte> GetCompte(int idCompte);
        Task<Compte> AddCompte(Compte Compte);
        Task<Compte> UpdateCompte(Compte Compte);
        Task<Compte> DeleteCompte(int idCompte);
        Task<IEnumerable<Compte>> SearchCompte(string name);
    }
}