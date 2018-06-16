using Proj002_140618.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proj002_140618.Persistence
{
    public class PedidosDeVenda : MainPersistence<PedidoDeVenda>
    {

        public List<PedidoDeVenda> Search(string NomeCliente, decimal? NumeroPedido, DateTime? DataEmissao)
        {
            var search = GetQueryable(); ;

            if (!string.IsNullOrEmpty(NomeCliente))
            {
                search = search.Where(w => w.Cliente.NomePessoa.Contains(NomeCliente));
            }

            if (NumeroPedido.GetValueOrDefault(0) != 0)
            {
                search = search.Where(w => w.NumeroPedido == NumeroPedido.Value);
            }

            if (DataEmissao.HasValue)
            {
                search = search.Where(w => w.DataEmissao == DataEmissao.Value);
            }

            return search.ToList();
        }

        public int ObtemProximoNroPedido()
        {
            var proxNro = GetQueryable().Max(pv => pv.NumeroPedido);
            return proxNro + 1;
        }

        public IEnumerable<Pessoa> GetPessoas()
        {
            return db.Pessoas.AsEnumerable();
        }

        public IEnumerable<ItemPedidoDeVenda> GetItemPedidoDeVenda()
        {
            return db.ItemPedidoDeVendas.AsEnumerable();
        }

    }
}