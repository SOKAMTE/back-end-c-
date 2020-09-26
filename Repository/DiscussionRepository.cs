using offreService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using offreService.Models;
using System.Linq;

namespace offreService.Repository
{
    public class DiscussionRepository: IDiscussionRepository
    {
        private readonly OffreServiceContext _context;

        public DiscussionRepository(OffreServiceContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<Discussion>> GetDiscussions()
        {
            return await _context.Discussions.ToListAsync();
        }

        public async Task<Discussion> GetDiscussion(int idDiscussion)
        {
            return await _context.Discussions
                .FirstOrDefaultAsync(e => e.idDiscussion == idDiscussion);
        }

        public async Task<Discussion> AddDiscussion(Discussion discussion)
        {
            var result = await _context.Discussions.AddAsync(discussion);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Discussion> UpdateDiscussion(Discussion discussion)
        {
            var result = await _context.Discussions
                .FirstOrDefaultAsync(e => e.idDiscussion == discussion.idDiscussion);

            if (result != null)
            {
                result.idDiscussion = discussion.idDiscussion;
                result.message = discussion.message;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Discussion> DeleteDiscussion(int idDiscussion)
        {
            var result = await _context.Discussions
                .FirstOrDefaultAsync(e => e.idDiscussion == idDiscussion);
            if (result != null)
            {
                _context.Discussions.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Discussion>> SearchDiscussion(string name)
        {
            IQueryable<Discussion> query = _context.Discussions;
                
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.message.Contains(name));
            }

            return await query.ToListAsync();
        }
    }
}