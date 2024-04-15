using CoundAndEat.Api.Entities;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Ingridient;

namespace CoundAndEat.Api.Interfaces
{
    public interface IIngridientRepository
    {
        Task<List<Ingridient>> GetIngridients();
        Task<Ingridient> GetIngridient(long id);
        public Task<Ingridient> AddNewIngridient(IngridientDto ingridientDto);
        public Task<Ingridient> UpdateIngridient(long id, UpdateIngridientDto updateIngDto);
        public Task<Status> DeleteIngridient(long id);
    }
}
