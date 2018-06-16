namespace Proj002_140618.Persistence
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entidades;

    public partial class ModelContext : DbContext
    {
        public ModelContext()
            : base("name=ModelContext")
        {
        }

        public virtual DbSet<ItemPedidoDeVenda> ItemPedidoDeVendas { get; set; }
        public virtual DbSet<PedidoDeVenda> PedidoDeVendas { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemPedidoDeVenda>()
                .Property(e => e.Qtde)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ItemPedidoDeVenda>()
                .Property(e => e.PrecoUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ItemPedidoDeVenda>()
                .Property(e => e.PercentualDesconto)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ItemPedidoDeVenda>()
                .Property(e => e.ValorTotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PedidoDeVenda>()
                .Property(e => e.ValorTotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.PedidosDeVenda)
                .WithRequired(e => e.Cliente)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Produto>()
                .Property(e => e.PrecoUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Produto>()
                .HasMany(e => e.ItensDePedidosDeVenda)
                .WithRequired(e => e.Produto)
                .WillCascadeOnDelete(false);
        }
    }
}
