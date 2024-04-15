using System.ComponentModel.DataAnnotations.Schema;

namespace CoundAndEat.Api.Entities
{
    public class ReceptIngridient
    {
        public long Id { get; set; }
        public long ReceptId { get; set; }
        
        public long IngridientId { get; set; }
        public double Quantity { get; set; }

        [ForeignKey("ReceptId")]
        public virtual Recept Recept { get; set; }

        [ForeignKey("IngridientId")]
        public virtual Ingridient Ingridient { get; set; }
    }
}
