using Proj002_140618.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proj002_140618.Persistence
{
    public class Pessoas : MainPersistence<Pessoa>
    {
        
        public List<Pessoa> Search(string NomePessoa, DateTime? DataNascimento)
        {
            var search = GetQueryable();

            if (!string.IsNullOrEmpty(NomePessoa))
            {
                search = search.Where(w => w.NomePessoa.Contains(NomePessoa));
            }

            if (DataNascimento.HasValue)
            {
                search = search.Where(w => w.DataNascimento == DataNascimento.Value);
            }

            return search.ToList();
        }
        
    }
}