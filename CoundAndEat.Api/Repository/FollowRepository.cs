using CoundAndEat.Api.Data;
using CoundAndEat.Api.Entities;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Follow;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoundAndEat.Api.Repository
{
    public class FollowRepository : IFollowRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public FollowRepository(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<bool> FollowItemExists(long userId, long receptId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return false;
            }
            var response = await _context.Follows.AnyAsync(c => c.UserId == user.Id &&
                                                                     c.ReceptId == receptId);
            return response;

        }

        public async Task<Follow> AddFollowItem(FollowToAddDto followToAddDto)
        {
            if (await FollowItemExists(followToAddDto.UserId, followToAddDto.ReceptId) == false)
            {
                var item = await(from recept in _context.Recepts
                                 where recept.Id == followToAddDto.ReceptId
                                 select new Follow
                                 {
                                     UserId = followToAddDto.UserId,
                                     ReceptId = recept.Id,
                                 }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await _context.Follows.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return default;
        }

        public async Task<Follow> DeleteFollowItem(long id)
        {
            var item = await _context.Follows.FindAsync(id);

            if (item != null)
            {
                _context.Follows.Remove(item);
                await _context.SaveChangesAsync();
            }

            return item;
        }

        public async Task<Follow> GetFollowItem(long id)
        {
            return await _context.Follows.Where(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<Follow>> GetFollowItems(long userId)
        {
            List<Follow> follows = await _context.Follows.Where(_ => _.UserId == userId).ToListAsync();
            return follows;
        }
    }
}
