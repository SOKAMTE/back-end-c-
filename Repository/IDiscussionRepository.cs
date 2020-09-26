using offreService.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace offreService.Repository
{
    public interface IDiscussionRepository
    {
        Task<IEnumerable<Discussion>> GetDiscussions();
        Task<Discussion> GetDiscussion(int idDiscussion);
        Task<Discussion> AddDiscussion(Discussion Discussion);
        Task<Discussion> UpdateDiscussion(Discussion Discussion);
        Task<Discussion> DeleteDiscussion(int idDiscussion);
        Task<IEnumerable<Discussion>> SearchDiscussion(string name);
    }
}