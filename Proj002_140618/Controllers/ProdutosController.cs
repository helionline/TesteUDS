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
    public class ProdutosController : Controller
    {
        private Produtos persistencia = new Produtos();

        // GET: Produtos
        public ActionResult Index()
        {
            return View(persistencia.List());
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = persistencia.Get(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduto,CodigoProduto,NomeProduto,PrecoUnitario")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                persistencia.Add(produto);
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = persistencia.Get(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduto,CodigoProduto,NomeProduto,PrecoUnitario")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                persistencia.Edit(produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = persistencia.Get(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = persistencia.Get(id);
            persistencia.Delete(produto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PesquisaProduto(string CodigoProduto, string NomeProduto)
        {
            var resultado = persistencia.Search(CodigoProduto, NomeProduto);
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
    }
}
