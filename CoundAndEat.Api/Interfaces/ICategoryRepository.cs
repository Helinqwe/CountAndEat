using CoundAndEat.Api.Entities;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Category;

namespace CoundAndEat.Api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(long id);
        Task<List<Recept>> GetReceptsByCategory(long catid);
        Task<Category> AddNewCategory(CategoryDto createCatDto);
        Task<Category> UpdateCategory(long id, UpdateCategoryDto updateCatDto);
        Task<Status> DeleteCategory(long id);
    }
}
