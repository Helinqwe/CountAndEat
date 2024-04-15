using CoundAndEat.Api.Entities;
using CoundAndEat.Dtos.Category;
using CoundAndEat.Dtos.Follow;
using CoundAndEat.Dtos.Ingridient;
using CoundAndEat.Dtos.Recept;
using CoundAndEat.Dtos.Recept.Description;
using CoundAndEat.Dtos.Recept.Image;
using CoundAndEat.Dtos.Recept.Ingridient;

namespace CoundAndEat.Api.Extension
{
    public static class DtoConvensions
    {
        ///////// Category /////////
        public static List<CategoryDto> ConvertToDto(this List<Category> categories)
        {
            if (categories != null)
            {
                return (from category in categories
                        select new CategoryDto
                        {
                            Id = category.Id,
                            Name = category.Name,
                            Image = category.Image,
                        }).ToList();
            }
            return null;
        }

        public static CategoryDto ConverToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image
            };
        }

        ///////// Ingridient /////////
        public static List<IngridientDto> ConvertToDto(this List<Ingridient> ingridients)
        {
            if (ingridients != null)
            {
                return (from ingridient in ingridients
                        select new IngridientDto
                        {
                            Id = ingridient.Id,
                            Name = ingridient.Name,
                            Measure= ingridient.Measure,
                        }).ToList();
            }
            return null;
        }

        public static IngridientDto ConverToDto(this Ingridient ingridient)
        {
            return new IngridientDto
            {
                Id = ingridient.Id,
                Name = ingridient.Name,
                Measure = ingridient.Measure
            };
        }

        /////////////// Folows ///////////////
        ///

        public static List<FollowDto> ConvertToDto(this List<Follow> followItems,
                                                           List<Recept> recepts)
        {
            return (from followItem in followItems
                    join recept in recepts
                    on followItem.ReceptId equals recept.Id
                    select new FollowDto
                    {
                        Id = followItem.Id,
                        ReceptId = followItem.ReceptId,
                        ReceptName = recept.Name,
                        ReceptImage = recept.Image,
                        UserId = followItem.UserId,
                    }).ToList();
        }

        public static FollowDto ConvertToDto(this Follow followItem,
                                                    Recept recept)
        {
            return new FollowDto
            {
                Id = followItem.Id,
                ReceptId = followItem.ReceptId,
                ReceptName = recept.Name,
                ReceptImage = recept.Image,
                UserId = followItem.UserId
            };
        }

        //////////// ReceptImages ////////////
        public static List<ImageDto> ConvertToDto(this List<ReceptImages> images)
        {
            return (from image in images
                    select new ImageDto
                    {
                        Id = image.Id,
                        UrlAdress = image.UrlAdress,
                        ReceptId = image.ReceptId
                    }).ToList();
        }

        public static ImageDto ConvertToDto(this ReceptImages image)
        {
            return new ImageDto
            {
                Id = image.Id,
                UrlAdress = image.UrlAdress,
                ReceptId = image.ReceptId,
            };
        }
        //////////// ReceptDescriptions ////////////
        public static List<DescriptionDto> ConvertToDto(this List<ReceptDescription> descriptions)
        {
            return (from description in descriptions
                    select new DescriptionDto
                    {
                        Id = description.Id,
                        Title = description.Title,
                        Description = description.Description,
                        ReceptId = description.ReceptId
                    }).ToList();
        }

        public static DescriptionDto ConvertToDto(this ReceptDescription description)
        {
            return new DescriptionDto
            {
                Id = description.Id,
                Title = description.Title,
                Description = description.Description,
                ReceptId = description.ReceptId,
            };
        }
        /////////////// Recept //////////////////
        public static List<ReceptDto> ConvertToDto(this List<Recept> recepts)
        {
            return (from recept in recepts
                    select new ReceptDto
                    {
                        Id = recept.Id,
                        Name = recept.Name,
                        Image = recept.Image,
                        Colories = recept.Colories,
                        CategoryId= recept.Category.Id,
                        CategoryName= recept.Category.Name
                    }).ToList();
        }

        public static ReceptFullDto ConvertToDto(this Recept recept)
        {
            return new ReceptFullDto
            {
                Id = recept.Id,
                Name = recept.Name,
                Image = recept.Image,
                Colories = recept.Colories,
                CategoryId = recept.Category.Id,
                CategoryName = recept.Category.Name,
                Descriptions = recept.Description.ConvertToDto(),
                Images = recept.Images.ConvertToDto(),
                Ingridiens = recept.Ingridients.ConvertToDto()
            };
        }
        /////////////// ReceptIngridient //////////////////
        public static List<ReceptIngridientDto> ConvertToDto(this List<ReceptIngridient> recIngs)
        {
            return (from recIng in recIngs
                    select new ReceptIngridientDto
                    {
                        Id = recIng.Id,
                        ReceptId = recIng.Recept.Id,
                        ReceptName= recIng.Recept.Name,
                        IngridientId= recIng.Ingridient.Id,
                        IngridientName = recIng.Ingridient.Name,
                        Measure = recIng.Ingridient.Measure,
                        Quantity = recIng.Quantity
                    }).ToList();
        }

        public static ReceptIngridientDto ConvertToDto(this ReceptIngridient recIng)
        {
            return new ReceptIngridientDto
            {
                Id = recIng.Id,
                ReceptId = recIng.Recept.Id,
                ReceptName = recIng.Recept.Name,
                IngridientId = recIng.Ingridient.Id,
                IngridientName = recIng.Ingridient.Name,
                Measure = recIng.Ingridient.Measure,
                Quantity = recIng.Quantity
            };
        }
    }
}
