namespace Comandas.API.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public List<ComandaItem> Items { get; set; } = new List<ComandaItem>();
    }
}
