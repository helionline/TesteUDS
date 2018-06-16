using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proj002_140618.Models;

namespace UnitTests
{
    [TestClass]
    public class uPedidoDeVenda
    {
        ModelContext db;
        PedidoDeVenda pedidoDeVendaTeste;
        Pessoa cliente;
        ItemPedidoDeVenda itemPedido;
        Produto produto;

        [TestInitialize]
        public void Prepara()
        {
            db = new ModelContext();

            //cliente
            cliente = new Pessoa();
            cliente.NomePessoa = "NomePessoa";
            cliente.DataNascimento = DateTime.Now;

            //pedido de venda
            pedidoDeVendaTeste = new PedidoDeVenda();
            pedidoDeVendaTeste.NumeroPedido = 1;
            pedidoDeVendaTeste.DataEmissao = DateTime.Now;
            pedidoDeVendaTeste.PedidoPronto = StatusPedido.PedidoIncompleto;

            //produto
            produto = new Produto();
            produto.NomeProduto = "NomeProduto";
            produto.PrecoUnitario = 5;

            //item do pedido
            itemPedido = new ItemPedidoDeVenda();
            itemPedido.Qtde = 2;
            itemPedido.PercentualDesconto = 0;
            itemPedido.Produto = produto;
        }

        [TestMethod]
        public void TentandoAdicionarPedidoSemNumero()
        {
            pedidoDeVendaTeste.NumeroPedido = 0;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Pessoas.Add(cliente);
                    db.SaveChanges();

                    pedidoDeVendaTeste.IdCliente = cliente.IdPessoa;
                    db.PedidoDeVendas.Add(pedidoDeVendaTeste);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Assert.IsNotNull(e, e.Message);
                }

                transaction.Rollback();
            }
        }

        [TestMethod]
        public void VerificaValorTotalDoPedido()
        {
            //o item do pedido adicionado tem qtde = 2 e valor unitario = 5
            pedidoDeVendaTeste.ItensDoPedidoDeVenda.Add(itemPedido);
            Assert.AreEqual(pedidoDeVendaTeste.ValorTotal, 10);
        }

        [TestCleanup]
        public void Limpeza()
        {
            db = null;
            pedidoDeVendaTeste = null;
            cliente = null;
            produto = null;
            itemPedido = null;
        }
    }
}
