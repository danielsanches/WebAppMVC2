namespace WebAppMVC2.Models
{
    public class ModelTest
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string Nome { get; set; }
        public List<ItensGrid> ItensGrid { get; set; } = new List<ItensGrid>();
    }     
}
