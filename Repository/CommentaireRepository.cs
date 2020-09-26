using offreService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using offreService.Models;
using System.Linq;

namespace offreService.Repository
{
    public class CommentaireRepository: ICommentaireRepository
    {
        private readonly OffreServiceContext _context;

        public CommentaireRepository(OffreServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<Commentaire>> GetCommentaires()
        {
            return await _context.Commentaires.ToListAsync();
        }

        public async Task<Commentaire> GetCommentaire(int idCommentaire)
        {
            return await _context.Commentaires
                .FirstOrDefaultAsync(e => e.idCommentaire == idCommentaire);
        }

        public async Task<Commentaire> AddCommentaire(Commentaire commentaire)
        {
            var result = await _context.Commentaires.AddAsync(commentaire);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Commentaire> UpdateCommentaire(Commentaire commentaire)
        {
            var result = await _context.Commentaires
                .FirstOrDefaultAsync(e => e.idCommentaire == commentaire.idCommentaire);

            if (result != null)
            {
                result.idCommentaire = commentaire.idCommentaire;
                result.message = commentaire.message;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Commentaire> DeleteCommentaire(int idCommentaire)
        {
            var result = await _context.Commentaires
                .FirstOrDefaultAsync(e => e.idCommentaire == idCommentaire);
            if (result != null)
            {
                _context.Commentaires.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Commentaire>> SearchCommentaire(string name)
        {
            IQueryable<Commentaire> query = _context.Commentaires;
                
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.message.Contains(name));
            }

            return await query.ToListAsync();
        }
    }
}