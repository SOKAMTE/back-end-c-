using offreService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace offreService.Repository
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticle(int idArticle);
        Task<Article> AddArticle(Article article);
        Task<Article> UpdateArticle(Article article);
        Task<Article> DeleteArticle(int idArticle);
        Task<IEnumerable<Article>> SearchArticle(string name);
    }
}