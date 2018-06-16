using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proj002_140618.Models;

namespace Proj002_140618.Controllers
{
    public class ItensPedidosDeVendaController : Controller
    {
        private ModelContext db = new ModelContext();

        // GET: ItensPedidosDeVenda
        public ActionResult Index()
        {
            var itemPedidoDeVendas = db.ItemPedidoDeVendas.Include(i => i.PedidoDeVenda).Include(i => i.Produto);
            return View(itemPedidoDeVendas.ToList());
        }

        // GET: ItensPedidosDeVenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPedidoDeVenda itemPedidoDeVenda = db.ItemPedidoDeVendas.Find(id);
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
                db.ItemPedidoDeVendas.Add(itemPedidoDeVenda);
                db.SaveChanges();
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
            ItemPedidoDeVenda itemPedidoDeVenda = db.ItemPedidoDeVendas.Find(id);
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
                db.Entry(itemPedidoDeVenda).State = EntityState.Modified;
                db.SaveChanges();
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
            ItemPedidoDeVenda itemPedidoDeVenda = db.ItemPedidoDeVendas.Find(id);
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
            ItemPedidoDeVenda itemPedidoDeVenda = db.ItemPedidoDeVendas.Find(id);
            db.ItemPedidoDeVendas.Remove(itemPedidoDeVenda);
            db.SaveChanges();
            return RedirectToAction("Edit", "PedidosDeVenda", new { id = itemPedidoDeVenda.IdPedidoVenda });
        }

        public object ObtemPrecoUnitarioProduto(int? idProduto)
        {
            decimal precoUnitario = 0;
            if(idProduto.HasValue)
            {
                try
                {
                    precoUnitario = db.Produtos.Find(idProduto.Value).PrecoUnitario;
                }
                catch (Exception e) { }
            }

            return precoUnitario;
        }

        private void PreparaListaDeProdutos(object produtoSelecionado = null)
        {
            var qProduto = from p in db.Produtos
                           orderby p.CodigoProduto
                           select p;

            ViewBag.listaProdutos = new SelectList(qProduto, "IdProduto", "NomeProduto", produtoSelecionado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
