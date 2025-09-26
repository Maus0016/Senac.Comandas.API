namespace Comandas.API.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; } // 0 - Livre, 1 - Ocupada, 2 - Reservada
    }
}
