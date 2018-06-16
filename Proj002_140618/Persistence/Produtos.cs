using Proj002_140618.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proj002_140618.Persistence
{
    public class Produtos : MainPersistence<Produto>
    {
        public List<Produto> Search(string CodigoProduto, string NomeProduto)
        {
            var search = GetQueryable();

            if (!string.IsNullOrEmpty(CodigoProduto))
            {
                search = search.Where(w => w.CodigoProduto == CodigoProduto);
            }

            if (!string.IsNullOrEmpty(NomeProduto))
            {
                search = search.Where(w => w.NomeProduto.Contains(NomeProduto));
            }

            return search.ToList();
        }
    }
}