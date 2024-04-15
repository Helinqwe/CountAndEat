using CoundAndEat.Api.Entities;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Recept;
using CoundAndEat.Dtos.Recept.Description;
using CoundAndEat.Dtos.Recept.Image;
using CoundAndEat.Dtos.Recept.Ingridient;

namespace CoundAndEat.Api.Interfaces
{
    public interface IReceptRepository
    {
        Task<List<Recept>> GetRecepts();
        Task<Recept> GetRecept(long id);
        public Task<Recept> AddNewRecept(CreateReceptDto receptDto);
        public Task<Recept> UpdateRecept(long id, UpdateReceptDto receptDto);
        public Task<Status> DeleteRecept(long id);

        public Task<ReceptImages> GetImage(long id);
        public Task<List<ReceptImages>> GetImagesByRecept(long receptId);
        public Task<ReceptImages> AddNewImage(ImageDto imageDto);
        public Task<ReceptImages> UpdateImage(long imgId, UpdateImageDto updateImage);
        public Task<Status> DeleteImage(long id);

        public Task<ReceptDescription> GetDescription(long id);
        public Task<List<ReceptDescription>> GetDescriptionsByRecept(long receptId);
        public Task<ReceptDescription> AddNewDescription(DescriptionDto descDto);
        public Task<ReceptDescription> UpdateDescription(long descId, UpdateDescriptionDto updateImage);
        public Task<Status> DeleteDescription(long descId);

        public Task<List<ReceptIngridient>> GetIngridientsByRecept(long receptId);
        public Task<List<Recept>> GetReceptByIngridients(FindIngridientDto ingridientIds);
        public Task<List<Recept>> GetReceptsByColories(FindColoriesDto findColories);
    }
}
