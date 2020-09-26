using offreService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using offreService.Models;
using System.Linq;

namespace offreService.Repository
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly OffreServiceContext _context;

        public ArticleRepository(OffreServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article> GetArticle(int idArticle)
        {
            return await _context.Articles
                .FirstOrDefaultAsync(e => e.IdArticle == idArticle);
        }

        public async Task<Article> AddArticle(Article article)
        {
            var result = await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Article> UpdateArticle(Article article)
        {
            var result = await _context.Articles
                .FirstOrDefaultAsync(e => e.IdArticle == article.IdArticle);

            if (result != null)
            {
                result.IdArticle = article.IdArticle;
                result.nom = article.nom;
                result.prix = article.prix;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Article> DeleteArticle(int idArticle)
        {
            var result = await _context.Articles
                .FirstOrDefaultAsync(e => e.IdArticle == idArticle);
            if (result != null)
            {
                _context.Articles.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Article>> SearchArticle(string name)
        {
            IQueryable<Article> query = _context.Articles;
                
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.nom.Contains(name));
            }

            return await query.ToListAsync();
        }
    }
}