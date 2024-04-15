using CoundAndEat.Api.Data;
using CoundAndEat.Api.Entities;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Ingridient;
using Microsoft.EntityFrameworkCore;

namespace CoundAndEat.Api.Repository
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly DataContext _context;
        public IngridientRepository(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> IngridientExist(string name)
        {
            return await _context.Ingridients.AnyAsync(c => c.Name == name);
        }
        private async Task<bool> IngridientExist(long id)
        {
            return await _context.Ingridients.AnyAsync(c => c.Id == id);
        }

        public async Task<Ingridient> AddNewIngridient(IngridientDto ingridientDto)
        {
            if (await IngridientExist(ingridientDto.Name) == false)
            {
                Ingridient ingridient = new Ingridient
                {
                    Name = ingridientDto.Name,
                    Measure= ingridientDto.Measure,
                };
               
                if (ingridient != null)
                {
                    var result = await _context.Ingridients.AddAsync(ingridient);
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }

            }
            return default;
        }

        public async Task<Status> DeleteIngridient(long id)
        {
            var ingridient = await _context.Ingridients.FindAsync(id);
            if (ingridient != null)
            {
                _context.Ingridients.Remove(ingridient);
                await _context.SaveChangesAsync();
                return new Status { Message = "Ингридиент успешно удален", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<Ingridient> GetIngridient(long id)
        {
            if (await IngridientExist(id))
            {
                var ingridient = await _context.Ingridients.SingleOrDefaultAsync(m => m.Id == id);
                return ingridient;
            }
            return default;
        }

        public async Task<List<Ingridient>> GetIngridients()
        {
            var ingridients = await _context.Ingridients.ToListAsync();
            return ingridients;
        }

        public async Task<Ingridient> UpdateIngridient(long id, UpdateIngridientDto updateIngDto)
        {
            var ingridient = await _context.Ingridients.FindAsync(id);
            if (ingridient != null)
            {
                ingridient.Name = updateIngDto.Name;
                ingridient.Measure = updateIngDto.Measure;
                await _context.SaveChangesAsync();
                return ingridient;
            }
            return default;
        }
    }
}
