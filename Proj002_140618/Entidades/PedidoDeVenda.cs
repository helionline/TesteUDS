namespace Proj002_140618.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PedidoDeVenda")]
    public partial class PedidoDeVenda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PedidoDeVenda()
        {
            ItensDoPedidoDeVenda = new HashSet<ItemPedidoDeVenda>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedidoVenda { get; set; }

        public int IdCliente { get; set; }

        [Display(Name ="Nro. do Pedido")]
        [Range(1, int.MaxValue)]
        public int NumeroPedido { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Data de Emissão")]
        public DateTime DataEmissao { get; set; }

        private decimal _ValorTotal;

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal
        {
            get
            {
                if (ItensDoPedidoDeVenda != null && ItensDoPedidoDeVenda.Count > 0)
                {
                    _ValorTotal = 0;
                    foreach (var item in ItensDoPedidoDeVenda)
                    {
                        _ValorTotal += item.ValorTotal;
                    }
                }

                return _ValorTotal;
            }
            set
            {
                this._ValorTotal = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name ="Itens")]
        public virtual ICollection<ItemPedidoDeVenda> ItensDoPedidoDeVenda { get; set; }

        [Display(Name ="Cliente")]
        public virtual Pessoa Cliente { get; set; }

        public StatusPedido PedidoPronto { get; set; }
    }
}
