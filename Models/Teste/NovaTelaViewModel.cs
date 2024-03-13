namespace WebAppMVC2.Models.Teste
{
    public class NovaTelaViewModel
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string Nome { get; set; }
        public List<ItensGridViewModel> ItensGrid { get; set; } = new List<ItensGridViewModel>();
    }     
}
