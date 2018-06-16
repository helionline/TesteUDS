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
    public class PedidosDeVendaController : Controller
    {
        PedidosDeVenda persistencia = new PedidosDeVenda();

        // GET: PedidosDeVenda
        public ActionResult Index()
        {
            return View(persistencia.List());
        }

        // GET: PedidosDeVenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDeVenda pedidoDeVenda = persistencia.Get(id);
            if (pedidoDeVenda == null)
            {
                return HttpNotFound();
            }
            return View(pedidoDeVenda);
        }

        // GET: PedidosDeVenda/Create
        public ActionResult Create()
        {
            PedidoDeVenda novoPedido = new PedidoDeVenda();
            novoPedido.NumeroPedido = persistencia.ObtemProximoNroPedido();
            novoPedido.DataEmissao = DateTime.Now;
            novoPedido.PedidoPronto = StatusPedido.PedidoIncompleto;
            PreparaListaDeClientes();
            return View(novoPedido);
        }

        // POST: PedidosDeVenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdPedidoVenda,IdCliente,NumeroPedido,DataEmissao,ValorTotal")] PedidoDeVenda pedidoDeVenda)
        {
            if (ModelState.IsValid)
            {
                pedidoDeVenda.PedidoPronto = StatusPedido.PedidoIncompleto;
                persistencia.Add(pedidoDeVenda);
                return RedirectToAction("Create", "ItensPedidosDeVenda", new { idPedido = pedidoDeVenda.IdPedidoVenda });
            }

            PreparaListaDeClientes(pedidoDeVenda.IdCliente);
            return View(pedidoDeVenda);
        }

        // GET: PedidosDeVenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDeVenda pedidoDeVenda = persistencia.Get(id);
            if (pedidoDeVenda == null)
            {
                return HttpNotFound();
            }
            PreparaListaDeClientes(pedidoDeVenda.IdCliente);
            return View(pedidoDeVenda);
        }

        // POST: PedidosDeVenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdPedidoVenda,IdCliente,NumeroPedido,DataEmissao,ValorTotal")] PedidoDeVenda pedidoDeVenda)
        {
            bool possuiItens = persistencia.GetItemPedidoDeVenda().Any(a => a.IdPedidoVenda == pedidoDeVenda.IdPedidoVenda);
            if (ModelState.IsValid && possuiItens)
            {
                pedidoDeVenda.PedidoPronto = StatusPedido.PedidoCompleto;
                persistencia.Edit(pedidoDeVenda);
                return RedirectToAction("Index");
            }
            pedidoDeVenda.PedidoPronto = StatusPedido.PedidoIncompleto;
            PreparaListaDeClientes(pedidoDeVenda.IdCliente);
            return View(pedidoDeVenda);
        }

        // GET: PedidosDeVenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoDeVenda pedidoDeVenda = persistencia.Get(id);
            if (pedidoDeVenda == null)
            {
                return HttpNotFound();
            }
            return View(pedidoDeVenda);
        }

        // POST: PedidosDeVenda/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoDeVenda pedidoDeVenda = persistencia.Get(id);
            persistencia.Delete(pedidoDeVenda);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PesquisaPedido(string NomeCliente, decimal? NumeroPedido, DateTime? DataEmissao)
        {
            var resultado = persistencia.Search(NomeCliente, NumeroPedido, DataEmissao);
            return View("Index", resultado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                persistencia.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PreparaListaDeClientes(object clienteSelecionado = null)
        {
            var qCliente = from c in persistencia.GetPessoas()
                           orderby c.NomePessoa
                           select c;

            ViewBag.listaClientes = new SelectList(qCliente, "IdPessoa", "NomePessoa", clienteSelecionado);
        }
        
    }
}
