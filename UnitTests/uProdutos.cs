using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proj002_140618.Entidades;
using Proj002_140618.Persistence;

namespace UnitTests
{
    [TestClass]
    public class uProdutos
    {
        private ModelContext db;
        private Produto produtoTeste;

        [TestInitialize]
        public void Prepara()
        {
            db = new ModelContext();

            produtoTeste = new Produto();
            produtoTeste.CodigoProduto = "1";
            produtoTeste.NomeProduto = "NomeProduto";
            produtoTeste.PrecoUnitario = 1;
        }

        [TestMethod]
        public void TentandoAdicionarProdutoSemCodigo()
        {
            produtoTeste.CodigoProduto = null;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Produtos.Add(produtoTeste);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Assert.IsNotNull(e, e.Message);
                }

                transaction.Rollback();
            }
        }

        [TestCleanup]
        public void Limpeza()
        {
            db = null;
            produtoTeste = null;
        }
    }
}
