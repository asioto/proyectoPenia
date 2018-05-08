using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using PeniaBermeja.Models;

namespace proyectoPenia.Controllers
{
    public class Menu2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //ACESO PARA EDITAR LOS ENLACES
        public ActionResult _EditarEnlaces()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            IOrderedEnumerable<Enlace> EnlacesMenu2Ordenados = db.Enlaces.Where(x => x.enlacePadre == "Menu2").ToList().OrderBy(x => x.posicion); //Enlaces ordenados por la posicion
            return View(EnlacesMenu2Ordenados);
        }
        //ACCESO AL LOGO
        public ActionResult _EditarLogo()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            EnlaceMejorado enlaceMejorado = db.EnlacesMejorados.Find(db.EnlacesMejorados.Where(x => x.enlace.enlacePadre == "Menu2Logo").First().enlaceMejoradoId);
            ViewBag.logo = enlaceMejorado.imagen;

            if (enlaceMejorado.imagen == null) { ViewBag.logo = new Imagen(); }

            return View(db.EnlacesMejorados.Where(x => x.enlace.enlacePadre == "Menu2Logo").ToList());
        }



        // GET: Menu2
        public ActionResult Index()
        {
            return View();
        }

        // GET: Menu2/Details/5
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

        // GET: Menu2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu2/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnlaceId,texto,anteEnlace,accion,controlador,claseCss,categoriaEnlace,enlacePadre,posicion")] Enlace enlace)
        {
            if (ModelState.IsValid)
            {
                //SU PADRE ES MENU2
                enlace.enlacePadre = "Menu2";

                db.Enlaces.Add(enlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enlace);
        }

        // GET: Menu2/Edit/5
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

        // POST: Menu2/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnlaceId,texto,anteEnlace,accion,controlador,claseCss,categoriaEnlace,enlacePadre,posicion")] Enlace enlace)
        {
            if (ModelState.IsValid)
            {
                //SU PADRE ES MENU2
                enlace.enlacePadre = "Menu2";

                db.Entry(enlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enlace);
        }

        // GET: Menu2/Delete/5
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

        // POST: Menu2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enlace enlace = db.Enlaces.Find(id);
            db.Enlaces.Remove(enlace);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarLogo(IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                EnlaceMejorado MiLogo = db.EnlacesMejorados.Find(Int32.Parse(Request.Form["IdLogo"]));

                //Datos del enlace
                MiLogo.enlace.texto = Request.Form["TextoEnlace"];
                MiLogo.enlace.accion = Request.Form["AccionEnlace"];
                MiLogo.enlace.controlador = Request.Form["ControladorEnlace"];
                

                //Datos de la imagen
                string carpeta = @"/Content/UploadedImages"; //Dirección donde se guardan las imagenes

                if (files.First() != null) //Eliminar imagen si ya estba guardada
                {
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(carpeta) + "/" + MiLogo.imagen.nombre);
                        db.Imagenes.Remove(MiLogo.imagen);
                    }
                    catch
                    {

                    }

                }

                foreach (var image in files) //Guardar imagen 
                {
                    if (image != null && image.ContentLength > 0)
                    {
        
                        Imagen imagen = new Imagen();
                        imagen.GuardarImagen(image,carpeta,"LogoMenu2");
                        db.Imagenes.Add(imagen);
                        MiLogo.imagen = imagen;

                    }
                }

                 //GUARDAR DATOS
                 db.Entry(MiLogo).State = EntityState.Modified;
                 await db.SaveChangesAsync();
                 return RedirectToAction("Index");
            }
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
