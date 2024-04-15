namespace CoundAndEat.Api.Entities
{
    public class Ingridient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
        public virtual List<ReceptIngridient>? ReceptIngridients { get;set; }
    }
}
