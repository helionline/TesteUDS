using Proj002_140618.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Proj002_140618.Persistence
{
    public class ItensPedidoDeVenda : MainPersistence<ItemPedidoDeVenda>
    {

        public List<ItemPedidoDeVenda> FullList()
        {
            return db.ItemPedidoDeVendas.Include(i => i.PedidoDeVenda).Include(i => i.Produto).ToList();
        }

        public IEnumerable<Produto> GetProdutos()
        {
            return db.Produtos.AsEnumerable();
        }
    }
}