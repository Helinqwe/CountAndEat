using CoundAndEat.Api.Entities;
using CoundAndEat.Dtos.Follow;

namespace CoundAndEat.Api.Interfaces
{
    public interface IFollowRepository
    {
        Task<Follow> AddFollowItem(FollowToAddDto followToAddDto);
        Task<Follow> DeleteFollowItem(long id);
        Task<Follow> GetFollowItem(long id);
        Task<List<Follow>> GetFollowItems(long userId);
    }
}
