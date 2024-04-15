using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoundAndEat.Dtos.Recept.Ingridient
{
    public class FindIngridientDto
    {
        public List<FindReceptsByIngridientsDto> Ingtidients { get; set; }
    }
}
