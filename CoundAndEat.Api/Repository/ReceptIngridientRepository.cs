using CoundAndEat.Api.Data;
using CoundAndEat.Api.Entities;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Recept.Ingridient;
using Microsoft.EntityFrameworkCore;

namespace CoundAndEat.Api.Repository
{
    public class ReceptIngridientRepository : IReceptIngridientRepository
    {
        private readonly DataContext _context;
        public ReceptIngridientRepository(DataContext context)
        {
            _context = context;
        }
        private async Task<bool> ReceptIngridientExist(long recIngId)
        {
            return await _context.ReceptIngridients.AnyAsync(ri => ri.Id == recIngId);
        }

        public async Task<ReceptIngridient> AddNewIngridient(CreateReceptIngridientDto createRecIngDto)
        {
            if(await ReceptIngridientExist(createRecIngDto.Id) == false)
            {
                ReceptIngridient receptIngridient = new ReceptIngridient
                {
                    Id = createRecIngDto.Id,
                    ReceptId = createRecIngDto.ReceptId,
                    IngridientId = createRecIngDto.IngridientId,
                    Quantity = createRecIngDto.Quantity,
                };
                if(receptIngridient != null)
                {
                    var result = _context.ReceptIngridients.AddAsync(receptIngridient);
                    await _context.SaveChangesAsync();
                    return receptIngridient;
                }

            }
            return default;
        }

        public async Task<Status> DeleteIngridient(long id)
        {
            var recIng = await _context.ReceptIngridients.FindAsync(id);
            if(recIng != null)
            {
                _context.ReceptIngridients.Remove(recIng); 
                await _context.SaveChangesAsync();
                return new Status { Message = "Ингридиент рецепта успешно удаленa", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<List<ReceptIngridient>> GetIngridients()
        {
            var recIng = await _context.ReceptIngridients.Include(i => i.Ingridient).Include(r => r.Recept).ToListAsync();
            return recIng;
        }

        public async Task<ReceptIngridient> GetIngridient(long id)
        {
            if (await ReceptIngridientExist(id))
            {
                var recIng = await _context.ReceptIngridients.FindAsync(id);
                recIng.Recept = await _context.Recepts.FindAsync(recIng.ReceptId);
                recIng.Ingridient = await _context.Ingridients.FindAsync(recIng.IngridientId);
                return recIng;
            }
            return default;
        }

        public async Task<ReceptIngridient> UpdateIngridient(long id, UpdateReceptIngridientDto updateIngDto)
        {
            var recIng = await _context.ReceptIngridients.FindAsync(id);
            if (recIng != null)
            {
                recIng.ReceptId = updateIngDto.ReceptId;
                recIng.IngridientId = updateIngDto.IngridientId;
                recIng.Quantity = updateIngDto.Quantity;
                await _context.SaveChangesAsync();
                return recIng;
            }
            return default;
        }
    }
}
