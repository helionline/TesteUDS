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
    public class PessoasController : Controller
    {
        private Pessoas persistencia = new Pessoas();

        // GET: Pessoas
        public ActionResult Index()
        {
            return View(persistencia.List());
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = persistencia.Get(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPessoa,NomePessoa,DataNascimento")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                persistencia.Add(pessoa);
                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = persistencia.Get(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPessoa,NomePessoa,DataNascimento")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                persistencia.Edit(pessoa);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = persistencia.Get(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = persistencia.Get(id);
            persistencia.Delete(pessoa);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PesquisaPessoa(string NomePessoa, DateTime? DataNascimento)
        {
            var resultado = persistencia.Search(NomePessoa, DataNascimento);
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
