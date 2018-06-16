using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proj002_140618.Persistence;
using Proj002_140618.Entidades;

namespace Proj002_140618.Controllers
{
    public class ItensPedidosDeVendaController : Controller
    {
        private ItensPedidoDeVenda persistencia = new ItensPedidoDeVenda();

        // GET: ItensPedidosDeVenda
        public ActionResult Index()
        {
            var itemPedidoDeVendas = persistencia.FullList();
            return View(itemPedidoDeVendas);
        }

        // GET: ItensPedidosDeVenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPedidoDeVenda itemPedidoDeVenda = persistencia.Get(id);
            if (itemPedidoDeVenda == null)
            {
                return HttpNotFound();
            }
            return View(itemPedidoDeVenda);
        }

        // GET: ItensPedidosDeVenda/Create
        public ActionResult Create(int? idPedido)
        {
            ItemPedidoDeVenda novoItem = new ItemPedidoDeVenda();
            novoItem.IdPedidoVenda = idPedido.Value;
            PreparaListaDeProdutos();
            return View(novoItem);
        }

        // POST: ItensPedidosDeVenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdItem,IdPedidoVenda,IdProduto,Qtde,PrecoUnitario,PercentualDesconto,ValorTotal")] ItemPedidoDeVenda itemPedidoDeVenda)
        {
            if (ModelState.IsValid)
            {
                persistencia.Add(itemPedidoDeVenda);
                return RedirectToAction("Edit", "PedidosDeVenda", new { id = itemPedidoDeVenda.IdPedidoVenda });
            }

            PreparaListaDeProdutos(itemPedidoDeVenda.IdProduto);
            return View(itemPedidoDeVenda);
        }

        // GET: ItensPedidosDeVenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPedidoDeVenda itemPedidoDeVenda = persistencia.Get(id);
            if (itemPedidoDeVenda == null)
            {
                return HttpNotFound();
            }
            PreparaListaDeProdutos(itemPedidoDeVenda.IdProduto);
            return View(itemPedidoDeVenda);
        }

        // POST: ItensPedidosDeVenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdItem,IdPedidoVenda,IdProduto,Qtde,PrecoUnitario,PercentualDesconto,ValorTotal")] ItemPedidoDeVenda itemPedidoDeVenda)
        {
            if (ModelState.IsValid)
            {
                persistencia.Edit(itemPedidoDeVenda);
                return RedirectToAction("Edit", "PedidosDeVenda", new { id = itemPedidoDeVenda.IdPedidoVenda });
            }
            PreparaListaDeProdutos(itemPedidoDeVenda.IdProduto);
            return View(itemPedidoDeVenda);
        }

        // GET: ItensPedidosDeVenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPedidoDeVenda itemPedidoDeVenda = persistencia.Get(id);
            if (itemPedidoDeVenda == null)
            {
                return HttpNotFound();
            }
            return View(itemPedidoDeVenda);
        }

        // POST: ItensPedidosDeVenda/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemPedidoDeVenda itemPedidoDeVenda = persistencia.Get(id);
            persistencia.Delete(itemPedidoDeVenda);
            return RedirectToAction("Edit", "PedidosDeVenda", new { id = itemPedidoDeVenda.IdPedidoVenda });
        }

        public object ObtemPrecoUnitarioProduto(int? idProduto)
        {
            decimal precoUnitario = 0;
            if(idProduto.HasValue)
            {
                try
                {
                    precoUnitario = persistencia.GetProdutos().Single(s => s.IdProduto == idProduto.Value).PrecoUnitario;
                }
                catch (Exception e) { }
            }

            return precoUnitario;
        }

        private void PreparaListaDeProdutos(object produtoSelecionado = null)
        {
            var qProduto = from p in persistencia.GetProdutos()
                           orderby p.CodigoProduto
                           select p;

            ViewBag.listaProdutos = new SelectList(qProduto, "IdProduto", "NomeProduto", produtoSelecionado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                persistencia.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
