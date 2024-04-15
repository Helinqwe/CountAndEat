using System.ComponentModel.DataAnnotations.Schema;

namespace CoundAndEat.Api.Entities
{
    public class ReceptImages
    {
        public long Id { get; set; }
        public string UrlAdress { get; set; }
        public long ReceptId { get; set; }
        [ForeignKey("ReceptId")]
        public virtual Recept Recept { get; set; }
    }
}
