using CoundAndEat.Api.Entities;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Category;
using CoundAndEat.Dtos.Recept.Ingridient;

namespace CoundAndEat.Api.Interfaces
{
    public interface IReceptIngridientRepository
    {
        Task<List<ReceptIngridient>> GetIngridients();
        Task<ReceptIngridient> GetIngridient(long id);
        Task<ReceptIngridient> AddNewIngridient(CreateReceptIngridientDto createRecIngDto);
        Task<ReceptIngridient> UpdateIngridient(long id, UpdateReceptIngridientDto updateIngDto);
        Task<Status> DeleteIngridient(long id);
    }
}
