using Comandas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Comandas.API
{
    public class ComandasDbContext : DbContext
    {
        public ComandasDbContext(DbContextOptions<ComandasDbContext> options) : base(options)
        {
        }
        //DEFINIR ALGUMAS CONFIGURAÕES ADICIONAIS DO BANCO 
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Usuario>()
                .HasData(
                new Models.Usuario
                {
                    Id = 1,
                    Nome = "Admin",
                    Email = "admin@admin.com",
                    Senha = "123"
                }
                );
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais do modelo podem ser feitas aqui

            modelBuilder.Entity<Models.Mesa>()
                .HasData(
                new Models.Mesa
                {
                    Id = 1,
                    NumeroMesa = 1,
                    SituacaoMesa = (int)SituacaoMesa.Livre 
                },
                new Models.Mesa
                {
                    Id = 2,
                    NumeroMesa = 2,
                    SituacaoMesa = (int)SituacaoMesa.Ocupada
                },
                new Models.Mesa
                {
                    Id = 3,
                    NumeroMesa = 3,
                    SituacaoMesa = (int)SituacaoMesa.Reservada
                }

                );
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Models.CardapioItem>()
                .HasData(
                new Models.CardapioItem
                {
                    Id = 1,
                    Titulo = "Coxinha",
                    Descricao = "Coxinha de frango com catupiry",
                    Preco = 6.50m,
                    PossuiPreparo = true

                },
                new Models.CardapioItem
                {
                    Id = 2,
                    Titulo = "Refrigerante Lata",
                    Descricao = "Refrigerante Lata 350ml",
                    Preco = 5.00m,
                    PossuiPreparo = false
                },
                new Models.CardapioItem
                {
                    Id = 3,
                    Titulo = "Pizza Média",
                    Descricao = "Pizza média dois sabores com 8 pedaços",
                    Preco = 25.00m,
                    PossuiPreparo = true
                }

                );
            base.OnModelCreating(modelBuilder);


        }



        public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
        public DbSet<Models.Mesa> Mesas { get; set; } = default!;
        public DbSet<Models.Reserva> Reservas { get; set; } = default!;
        public DbSet<Models.Comanda> Comandas { get; set; } = default!;
        public DbSet<Models.ComandaItem> ComandaItems { get; set; } = default!;
        public DbSet<Models.PedidoCozinha> PedidoCozinhas { get; set; } = default!;
        public DbSet<Models.PedidoCozinhaItem> PedidoCozinhaItems { get; set; } = default!;
        public DbSet<Models.CardapioItem> CardapioItems { get; set; } = default!;


    }
}
