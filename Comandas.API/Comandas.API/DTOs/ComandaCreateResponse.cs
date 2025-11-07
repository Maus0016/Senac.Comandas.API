using Comandas.API.Models;

namespace Comandas.API.DTOs
{
    public class ComandaCreateResponse
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public List<ComandaItemResponse> Items { get; set; } = new List<ComandaItemResponse>();
    }
    public class ComandaItemResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
    }
}
