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
    public class SlidersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sliders
        public ActionResult Index()
        {
            IOrderedEnumerable<Slider> SliderOrdenados = db.Sliders.ToList().OrderBy(x => x.posicion); //Enlaces ordenados por la posicion
            return View(SliderOrdenados);
        }

        // GET: Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sliders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //Nos deja introducir texto con etiquetas
        public ActionResult Create([Bind(Include = "sliderId,titulo,texto,posicion")] Slider slider ,  HttpPostedFileBase Imagenr)
        {

            if (ModelState.IsValid)
            {

                //Creacion de boton1
                Enlace boton1 = new Enlace();
                boton1.texto = Request.Form["boton1.texto"];
                boton1.accion = Request.Form["boton1.accion"];
                boton1.controlador = Request.Form["boton1.controlador"];
                db.Enlaces.Add(boton1);
                slider.boton1 = boton1;

                //Creacion de boton1
                Enlace boton2 = new Enlace();
                boton2.texto = Request.Form["boton2.texto"];
                boton2.accion = Request.Form["boton2.accion"];
                boton2.controlador = Request.Form["boton2.controlador"];
                db.Enlaces.Add(boton1);
                slider.boton1 = boton1;

            //Creacion Imagen
            string carpeta = @"/Content/UploadedImages"; //Dirección donde se guardan las imagenes
            if (Imagenr != null && Imagenr.ContentLength > 0)
            {
                Imagen imagen = new Imagen();
                imagen.GuardarImagen(Imagenr, carpeta, "Slider");
                db.Imagenes.Add(imagen);
                slider.imagen = imagen;
                
              }

                db.Sliders.Add(slider); //Guardarnuevo slider con todos sus datos
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sliderId,titulo,texto,posicion")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
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
