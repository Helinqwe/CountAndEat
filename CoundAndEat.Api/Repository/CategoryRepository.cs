using CoundAndEat.Api.Data;
using CoundAndEat.Api.Entities;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Category;
using Microsoft.EntityFrameworkCore;

namespace CoundAndEat.Api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _DbContext;

        public CategoryRepository(DataContext tparfDbContext)
        {
            _DbContext = tparfDbContext;
        }

        private async Task<bool> CategoryExist(long catId)
        {
            return await _DbContext.Categories.AnyAsync(c => c.Id == catId);
        }

        public async Task<Category> AddNewCategory(CategoryDto createCatDto)
        {
            if (await CategoryExist(createCatDto.Id) == false)
            {
                Category category = new Category
                {
                    Name = createCatDto.Name,
                    Image = createCatDto.Image
                };
                if (category != null)
                {
                    var result = await _DbContext.Categories.AddAsync(category);
                    await _DbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<Status> DeleteCategory(long id)
        {
            var category = await _DbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _DbContext.Categories.Remove(category);
                await _DbContext.SaveChangesAsync();
                return new Status { Message = "Категория успешно удаленa", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<List<Category>> GetCategories()
        {

            var categories = await _DbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(long id)
        {
            if (await CategoryExist(id))
            {
                var category = await _DbContext.Categories.FindAsync(id);
                return category;
            }
            return null;
        }

        public async Task<List<Recept>> GetReceptsByCategory(long catid)
        {
            var subcategory = await _DbContext.Recepts.Include(s => s.Category).Where(s => s.CategoryId == catid).ToListAsync();
            return subcategory;
        }

        public async Task<Category> UpdateCategory(long id, UpdateCategoryDto updateCatDto)
        {
            var category = await _DbContext.Categories.FindAsync(id);
            if (category != null)
            {
                category.Name = updateCatDto.Name;
                category.Image = updateCatDto.Image;
                await _DbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
}
