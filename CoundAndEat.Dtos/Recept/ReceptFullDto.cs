using CoundAndEat.Dtos.Ingridient;
using CoundAndEat.Dtos.Recept.Description;
using CoundAndEat.Dtos.Recept.Image;
using CoundAndEat.Dtos.Recept.Ingridient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoundAndEat.Dtos.Recept
{
    public class ReceptFullDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Colories { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual List<ReceptIngridientDto> Ingridiens { get; set; }
        public virtual List<DescriptionDto> Descriptions { get; set; }
        public virtual List<ImageDto> Images { get; set; }
    }
}
