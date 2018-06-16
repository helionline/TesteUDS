namespace Proj002_140618.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Produto")]
    public partial class Produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto()
        {
            ItensDePedidosDeVenda = new HashSet<ItemPedidoDeVenda>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduto { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name="Código do Produto")]
        public string CodigoProduto { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name="Nome do Produto")]
        public string NomeProduto { get; set; }

        [Display(Name="Preço")]
        [DataType(DataType.Currency)]
        public decimal PrecoUnitario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemPedidoDeVenda> ItensDePedidosDeVenda { get; set; }
    }
}
