using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoundAndEat.Dtos.Recept.Ingridient
{
    public class CreateReceptIngridientDto
    {
        public long Id { get; set; }
        public long ReceptId { get; set; }
        public long IngridientId { get; set; }
        public double Quantity { get; set; }
    }
}
