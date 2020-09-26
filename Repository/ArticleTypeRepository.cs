using offreService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using offreService.Models;
using System.Linq;

namespace offreService.Repository
{
    public class ArticleTypeRepository: IArticleTypeRepository
    {
        private readonly OffreServiceContext _context;

        public ArticleTypeRepository(OffreServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<ArticleType>> GetArticleTypes()
        {
            return await _context.ArticleTypes.ToListAsync();
        }

        public async Task<ArticleType> GetArticleType(int idArticleType)
        {
            return await _context.ArticleTypes
                .FirstOrDefaultAsync(e => e.idArticleType == idArticleType);
        }

        public async Task<ArticleType> AddArticleType(ArticleType articleType)
        {
            var result = await _context.ArticleTypes.AddAsync(articleType);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ArticleType> UpdateArticleType(ArticleType articleType)
        {
            var result = await _context.ArticleTypes
                .FirstOrDefaultAsync(e => e.idArticleType == articleType.idArticleType);

            if (result != null)
            {
                result.idArticleType = articleType.idArticleType;
                result.nomType = articleType.nomType;
                result.dateDebut = articleType.dateDebut;
                result.dateFin = articleType.dateFin;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<ArticleType> DeleteArticleType(int idArticleType)
        {
            var result = await _context.ArticleTypes
                .FirstOrDefaultAsync(e => e.idArticleType == idArticleType);
            if (result != null)
            {
                _context.ArticleTypes.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<ArticleType>> SearchArticleType(string name)
        {
            IQueryable<ArticleType> query = _context.ArticleTypes;
                
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.nomType.Contains(name));
            }

            return await query.ToListAsync();
        }
    }
}