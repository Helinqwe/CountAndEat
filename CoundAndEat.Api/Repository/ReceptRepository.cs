using CoundAndEat.Api.Data;
using CoundAndEat.Api.Entities;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using CoundAndEat.Dtos.Recept;
using CoundAndEat.Dtos.Recept.Description;
using CoundAndEat.Dtos.Recept.Image;
using CoundAndEat.Dtos.Recept.Ingridient;
using Microsoft.EntityFrameworkCore;

namespace CoundAndEat.Api.Repository
{
    public class ReceptRepository : IReceptRepository
    {
        private readonly DataContext _context;
        public ReceptRepository(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> ReceptExist(long receptId)
        {
            return await _context.Recepts.AnyAsync(c => c.Id == receptId);
        }

        private async Task<bool> ReceptExist(string receptName)
        {
            return await _context.Recepts.AnyAsync(c => c.Name == receptName);
        }

        private async Task<bool> ImageExist(long imgId)
        {
            return await _context.ReceptImages.AnyAsync(c => c.Id == imgId);
        }

        private async Task<bool> DescriptionExist(long desId)
        {
            return await _context.ReceptDescriptions.AnyAsync(c => c.Id == desId);
        }

        public async Task<ReceptDescription> AddNewDescription(DescriptionDto descDto)
        {
            if (await DescriptionExist(descDto.Id) == false)
            {
                ReceptDescription description = new ReceptDescription
                {
                    Title = descDto.Title,
                    Description = descDto.Description,
                    ReceptId = descDto.ReceptId,
                };
                if (description != null)
                {
                    await _context.ReceptDescriptions.AddAsync(description);
                    await _context.SaveChangesAsync();
                    return description;
                }
            }
            return default;
        }

        public async Task<ReceptImages> AddNewImage(ImageDto imageDto)
        {
            if (await ImageExist(imageDto.Id) == false)
            {
                ReceptImages image = new ReceptImages
                {
                    UrlAdress= imageDto.UrlAdress,
                    ReceptId = imageDto.ReceptId
                };
                if (image != null)
                {
                    await _context.ReceptImages.AddAsync(image);
                    await _context.SaveChangesAsync();
                    return image;
                }
            }
            return default;
        }

        public async Task<Recept> AddNewRecept(CreateReceptDto receptDto)
        {
            if (await ReceptExist(receptDto.Id) == false && await ReceptExist(receptDto.Name) == false)
            {
                Recept recept = new Recept
                {
                    Name = receptDto.Name,
                    Image = receptDto.Image,
                    Colories = receptDto.Colories,
                    CategoryId = receptDto.CategoryId,
                };
                if (recept != null)
                {
                    await _context.Recepts.AddAsync(recept);
                    await _context.SaveChangesAsync();
                    return recept;
                }
            }
            return default;
        }

        public async Task<Status> DeleteDescription(long descId)
        {
            ReceptDescription description = await _context.ReceptDescriptions.FindAsync(descId);
            if (description != null)
            {
                _context.ReceptDescriptions.Remove(description);
                await _context.SaveChangesAsync();
                return new Status { Message = "Описание успешно удалено", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<Status> DeleteImage(long id)
        {
            ReceptImages image = await _context.ReceptImages.FindAsync(id);
            if (image != null)
            {
                _context.ReceptImages.Remove(image);
                await _context.SaveChangesAsync();
                return new Status { Message = "Картинка успешно удалена", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<Status> DeleteRecept(long id)
        {
            Recept recept = await _context.Recepts.FindAsync(id);
            if (recept != null)
            {
                _context.Recepts.Remove(recept);
                await _context.SaveChangesAsync();
                return new Status { Message = "Товар успешно удален", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<ReceptDescription> GetDescription(long id)
        {
            if (await DescriptionExist(id))
            {
                var description = await _context.ReceptDescriptions.SingleOrDefaultAsync(c => c.Id == id);
                description.ReceptId = GetRecept(description.ReceptId).Result.Id;
                return description;
            }
            return default;
        }

        public async Task<List<ReceptDescription>> GetDescriptionsByRecept(long repId)
        {
            var descriptions = await _context.ReceptDescriptions.Include(c => c.Recept).Where(c => c.ReceptId == repId).ToListAsync();
            return descriptions;
        }

        public async Task<ReceptImages> GetImage(long id)
        {
            if (await ImageExist(id))
            {
                var image = await _context.ReceptImages.SingleOrDefaultAsync(c => c.Id == id);
                image.ReceptId = GetRecept(image.ReceptId).Result.Id;
                return image;
            }
            return default;
        }

        public async Task<List<ReceptImages>> GetImagesByRecept(long receptId)
        {
            var images = await _context.ReceptImages.Include(c => c.Recept).Where(c => c.ReceptId == receptId).ToListAsync();
            return images;
        }

        public async Task<Recept> GetRecept(long id)
        {
            if (await ReceptExist(id))
            {
                var recept = await _context.Recepts.SingleOrDefaultAsync(c => c.Id == id);
                recept.Category = await _context.Categories.FindAsync(recept.CategoryId);
                if (recept.Images == null || recept.Description == null || recept.Ingridients == null)
                {
                    recept.Images = await GetImagesByRecept(id);
                    recept.Description = await GetDescriptionsByRecept(id);
                    recept.Ingridients = await GetIngridientsByRecept(id);
                }
                return recept;
            }
            return default;
        }

        public async Task<List<Recept>> GetRecepts()
        {
            var recepts = await _context.Recepts.Include(p => p.Category).ToListAsync();
            return recepts;
        }

        public async Task<ReceptDescription> UpdateDescription(long descId, UpdateDescriptionDto updateDescriptionDto)
        {
            ReceptDescription description = await _context.ReceptDescriptions.FindAsync(descId);
            if (description != null)
            {
                description.Title = updateDescriptionDto.Title;
                description.Description = updateDescriptionDto.Description;
                await _context.SaveChangesAsync();
                return description;
            }
            return default;
        }

        public async Task<ReceptImages> UpdateImage(long imgId, UpdateImageDto updateImage)
        {
            ReceptImages image = await _context.ReceptImages.FindAsync(imgId);
            if (image != null)
            {
                image.UrlAdress = updateImage.UrlAdress;
                await _context.SaveChangesAsync();
                return image;
            }
            return default;
        }

        public async Task<Recept> UpdateRecept(long id, UpdateReceptDto receptDto)
        {
            var recept = await _context.Recepts.FindAsync(id);
            if (recept != null)
            {
                recept.Name = receptDto.Name;
                recept.Image = receptDto.Image;
                recept.Colories = receptDto.Colories;
                recept.CategoryId = receptDto.CategoryId;
                await _context.SaveChangesAsync();
                return recept;
            }
            return default;
        }

        public async Task<List<ReceptIngridient>> GetIngridientsByRecept(long receptId)
        {
            var ingridients = await _context.ReceptIngridients.Include(c => c.Recept).Where(c => c.ReceptId == receptId).Include(i => i.Ingridient).ToListAsync();
            return ingridients;
        }

        public async Task<List<Recept>> GetReceptByIngridients(FindIngridientDto ingridientIds)
        {
            var count = 0;
            List<Recept> findedRecept = new();
            var recepts = await _context.Recepts.Include(i => i.Ingridients).Include(c => c.Category).ToListAsync();
            foreach (var ingridientId in ingridientIds.Ingtidients)
            {
                foreach (var recept in recepts)
                {
                    foreach(var recIng in recept.Ingridients)
                    {
                        if(recIng.Id == ingridientId.Id)
                        {
                            count++;
                        }
                    }
                    if (count == ingridientIds.Ingtidients.Count())
                    {
                        findedRecept.Add(recept);
                    }
                }
            }
            if (findedRecept != null)
                return findedRecept;
            return default;

        }

        public async Task<List<Recept>> GetReceptsByColories(FindColoriesDto findColories)
        {
            List<Recept> findedRecept = new();
            List<Recept> recepts = await _context.Recepts.Include(i => i.Ingridients).Include(c => c.Category).ToListAsync();
            foreach (var recept in recepts)
            {
                if((recept.Colories >= (findColories.Colories - 50) && recept.Colories <= (findColories.Colories + 50)))
                {
                    findedRecept.Add(recept);
                }
            }
            if (findedRecept != null)
                return findedRecept;
            return default;
        }
    }
}
