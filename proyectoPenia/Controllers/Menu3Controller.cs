using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using PeniaBermeja.Models;

namespace proyectoPenia.Controllers
{
    public class Menu3Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menu3
        public ActionResult Index()
        {
            IOrderedEnumerable<Enlace> EnlacesMenu3Ordenados = db.Enlaces.Where(x => x.enlacePadre == "Menu3").ToList().OrderBy(x => x.posicion); //Enlaces ordenados por la posicion
            return View(EnlacesMenu3Ordenados);
        }

        // GET: Menu3/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enlace enlace = db.Enlaces.Find(id);
            if (enlace == null)
            {
                return HttpNotFound();
            }
            return View(enlace);
        }

        // GET: Menu3/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu3/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnlaceId,texto,anteEnlace,accion,controlador,claseCss,categoriaEnlace,enlacePadre,posicion")] Enlace enlace)
        {
            if (ModelState.IsValid)
            {
                //SU PADRE ES MENU3
                enlace.enlacePadre = "Menu3";

                db.Enlaces.Add(enlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enlace);
        }

        // GET: Menu3/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enlace enlace = db.Enlaces.Find(id);
            if (enlace == null)
            {
                return HttpNotFound();
            }
            return View(enlace);
        }

        // POST: Menu3/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnlaceId,texto,anteEnlace,accion,controlador,claseCss,categoriaEnlace,enlacePadre,posicion")] Enlace enlace)
        {
            if (ModelState.IsValid)
            {
                //SU PADRE ES MENU3
                enlace.enlacePadre = "Menu3";

                db.Entry(enlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enlace);
        }

        // GET: Menu3/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enlace enlace = db.Enlaces.Find(id);
            if (enlace == null)
            {
                return HttpNotFound();
            }
            return View(enlace);
        }

        // POST: Menu3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enlace enlace = db.Enlaces.Find(id);
            db.Enlaces.Remove(enlace);
            db.SaveChanges();
            return RedirectToAction("Index");
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
