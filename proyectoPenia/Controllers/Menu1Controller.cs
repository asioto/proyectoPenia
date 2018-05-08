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
    public class Menu1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menu1
        public ActionResult Index()
        {
            IOrderedEnumerable<Enlace> EnlacesMenu1Ordenados = db.Enlaces.Where(x => x.enlacePadre == "Menu1").ToList().OrderBy(x => x.posicion); //Enlaces ordenados por la posicion
            return View(EnlacesMenu1Ordenados);
            
        }

        // GET: Menu1/Details/5
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

        // GET: Menu1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnlaceId,texto,anteEnlace,accion,controlador,claseCss,categoriaEnlace,enlacePadre,posicion")] Enlace enlace)
        {
            if (ModelState.IsValid)
            {

                //SU PADRE ES MENU1
                enlace.enlacePadre = "Menu1";

                db.Enlaces.Add(enlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enlace);
        }

        // GET: Menu1/Edit/5
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

        // POST: Menu1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnlaceId,texto,anteEnlace,accion,controlador,claseCss,categoriaEnlace,enlacePadre,posicion")] Enlace enlace)
        {
            if (ModelState.IsValid)
            {

                //SU PADRE ES MENU1
                enlace.enlacePadre = "Menu1";

                db.Entry(enlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enlace);
        }

        // GET: Menu1/Delete/5
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

        // POST: Menu1/Delete/5
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
