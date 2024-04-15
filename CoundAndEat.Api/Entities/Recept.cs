using System.ComponentModel.DataAnnotations.Schema;

namespace CoundAndEat.Api.Entities
{
    public class Recept
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int? Colories { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual List<ReceptIngridient>? Ingridients { get; set;}
        public virtual List<ReceptDescription>? Description { get; set; }
        public virtual List<ReceptImages>? Images { get; set; }
    }
}
