using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proj002_140618.Entidades;
using Proj002_140618.Persistence;

namespace UnitTests
{
    [TestClass]
    public class uPessoas
    {
        private ModelContext db;
        private Pessoa pessoaTeste;

        [TestInitialize]
        public void Prepara()
        {
            db = new ModelContext();

            pessoaTeste = new Pessoa();
            pessoaTeste.NomePessoa = "NomePessoa";
            pessoaTeste.DataNascimento = DateTime.Now;
        }

        [TestMethod]
        public void TentandoAdicionarPessoaSemNome()
        {
            pessoaTeste.NomePessoa = null;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Pessoas.Add(pessoaTeste);
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
            pessoaTeste = null;
        }
    }
}
