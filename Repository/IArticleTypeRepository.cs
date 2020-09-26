using offreService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace offreService.Repository
{
    public interface IArticleTypeRepository
    {
        Task<IEnumerable<ArticleType>> GetArticleTypes();
        Task<ArticleType> GetArticleType(int idArticleType);
        Task<ArticleType> AddArticleType(ArticleType ArticleType);
        Task<ArticleType> UpdateArticleType(ArticleType ArticleType);
        Task<ArticleType> DeleteArticleType(int idArticleType);
        Task<IEnumerable<ArticleType>> SearchArticleType(string name);
    }
}