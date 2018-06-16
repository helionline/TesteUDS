namespace Proj002_140618.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemPedidoDeVenda")]
    public partial class ItemPedidoDeVenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdItem { get; set; }

        public int IdPedidoVenda { get; set; }

        [Display(Name ="Produto")]
        public int IdProduto { get; set; }

        [Display(Name ="Qtde")]
        public decimal Qtde { get; set; }

        private decimal _PrecoUnitario;

        [Display(Name ="Valor Unitário")]
        [DataType(DataType.Currency)]
        public decimal PrecoUnitario {
            get
            {
                if(Produto != null)
                {
                    _PrecoUnitario = Produto.PrecoUnitario;
                }
                return _PrecoUnitario;
            }
            set
            {
                _PrecoUnitario = value;
            }
        }

        [Display(Name ="% Desconto")]
        public decimal PercentualDesconto { get; set; }

        private decimal _ValorTotal;

        [Display(Name ="Valor Total")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal {
            get
            {
                decimal valorComQtde = PrecoUnitario * Qtde;
                _ValorTotal = Decimal.Round(valorComQtde - (valorComQtde * (PercentualDesconto / 100)), 2);
                return _ValorTotal;
            }
            set
            {
                _ValorTotal = value;
            }
        }

        public virtual PedidoDeVenda PedidoDeVenda { get; set; }

        [Display(Name ="Produto")]
        public virtual Produto Produto { get; set; }
    }
}
