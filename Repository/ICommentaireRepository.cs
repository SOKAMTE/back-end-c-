using offreService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace offreService.Repository
{
    public interface ICommentaireRepository
    {
        Task<IEnumerable<Commentaire>> GetCommentaires();
        Task<Commentaire> GetCommentaire(int idCommentaire);
        Task<Commentaire> AddCommentaire(Commentaire Commentaire);
        Task<Commentaire> UpdateCommentaire(Commentaire Commentaire);
        Task<Commentaire> DeleteCommentaire(int idCommentaire);
        Task<IEnumerable<Commentaire>> SearchCommentaire(string name);
    }
}