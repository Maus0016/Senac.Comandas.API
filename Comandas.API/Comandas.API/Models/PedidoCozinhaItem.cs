using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comandas.API.Models
{
    public class PedidoCozinhaItem
    {
        [Key] //PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PedidoCozinhaId { get; set; } //FK
        public virtual PedidoCozinha PedidoCozinha { get; set; } // Navegação
        public int ComandaItemId { get; set; } //FK
        public virtual ComandaItem ComandaItem { get; set; } // Navegação


    }
}
