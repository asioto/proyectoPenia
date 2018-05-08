using IdentitySample.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace PeniaBermeja.Models
{
    public class Menu2
    {
        public Menu2()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            IOrderedEnumerable<Enlace> EnlacesMenu2 = db.Enlaces.Where(x => x.enlacePadre == "Menu2").ToList().OrderByDescending(x => x.posicion); //Enlaces ordenados por la posicion
            Logo = db.EnlacesMejorados.Where(x => x.enlace.enlacePadre == "Menu2").First();

            foreach (var item in EnlacesMenu2)
            {
                MisEnlaces.Add(item);
            }

        }



        public virtual ICollection<Enlace> MisEnlaces { get; set; }
        public virtual EnlaceMejorado Logo { get; set; }
    }

    public class Enlace
    {
        public int EnlaceId { get; set; }
        [Display(Name = "Texto")]
        public string texto { get; set; }
        [Display(Name = "Ante Enlace")]
        public string anteEnlace { get; set; }
        [Display(Name = "Acción")]
        public string accion { get; set; }
        [Display(Name = "Controlador")]
        public string controlador { get; set; }
        [Display(Name = "CalseCSS")]
        public string claseCss { get; set; }
        [Display(Name = "Categoria Del Enlace")]
        public string categoriaEnlace { get; set; }
        [Display(Name = "EnlacePadre")]
        public string enlacePadre { get; set; }
        [Display(Name = "Posición ")]
        public int posicion { get; set; }

        public string Imprimete()
        {
            if (this.controlador != null && this.controlador.Split('[', ']').Count() > 1)
            {
                if (this.controlador.Split('[', ']')[1] == "http" || this.controlador.Split('[', ']')[1] == "file" || this.controlador.Split('[', ']')[1] == "ftp")
                {
                    //La url es la accion : es una url externa
                    return this.accion.ToString();
                }
            }

            return this.controlador + "/" + this.accion;
        }


    }

    public class Imagen
    {
        public int imagenId { get; set; }
        public string carpeta { get; set; }
        public string nombre { get; set; }

        public string GuardarImagen(HttpPostedFileBase FileImagen, string Carpeta, string Categoria)
        {
            if (string.IsNullOrWhiteSpace(Categoria))
            {
                Categoria = "CategoriaPorDefecto";
            }
            if (string.IsNullOrWhiteSpace(Carpeta))
            {
                Carpeta = @"/Content/UploadedImages";
            }


            string NuevoNombre = Categoria + "_" + Path.GetFileName(FileImagen.FileName); //Cambiamos el nombre
            this.nombre = NuevoNombre;
            this.carpeta = Carpeta;

            FileImagen.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath(carpeta), this.nombre)); //Guardamos el nombre 

            return "OK";
        }

        public string imagenPath()
        {
            return this.carpeta + "/" + this.nombre;
        }
    }

    public class EnlaceMejorado
    {
        public int enlaceMejoradoId { get; set; }
        public virtual Enlace enlace { get; set; }
        [Display(Name = "Imagen")]
        public virtual Imagen imagen { get; set; }
    }

}