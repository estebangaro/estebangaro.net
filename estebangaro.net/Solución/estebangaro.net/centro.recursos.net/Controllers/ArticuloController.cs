using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Utileria;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace centro.recursos.net.Controllers
{
    public class ArticuloController : ControladorBase
    {
        // GET: Articulo
        public ActionResult CSharp(string id)
        {
            return View(viewName: id);
        }

        [ChildActionOnly]
        public PartialViewResult _ConvertidorCodigo(string rutaCodigos, 
            string idArticulo = null)
        {
            Respuesta<List<ClasePersonalizada>> clasesPersonalizadas = 
                Repositorio.ObtenClasesPersonalizadasCodigo(idArticulo);
            Respuesta<List<PalabraCodigo>> palabrascodigo =
                Repositorio.ObtenPalabrasCodigo();
            PartialViewResult resultado = null;

            if (clasesPersonalizadas.Estado && palabrascodigo.Estado)
                resultado = PartialView(new PalabrasCodigo
                {
                    RutaCodigoFuente = rutaCodigos,
                    ClasesPersonalizadas = clasesPersonalizadas
                        .Resultado
                        .Select(clase => clase.Nombre)
                        .ToList(),
                    PalabrasReservadas = palabrascodigo.Resultado
                });
            else if (!clasesPersonalizadas.Estado)
                ViewBag.LayoutExcepcion += (ViewBag.LayoutExcepcion != null ?
                    $"@{GeneraRespuestaExcepcion<List<ClasePersonalizada>>(clasesPersonalizadas)}" :
                    GeneraRespuestaExcepcion<List<ClasePersonalizada>>(clasesPersonalizadas));
            else if (!palabrascodigo.Estado)
                ViewBag.LayoutExcepcion += (ViewBag.LayoutExcepcion != null ?
                    $"@{GeneraRespuestaExcepcion<List<PalabraCodigo>>(palabrascodigo)}" :
                    GeneraRespuestaExcepcion<List<PalabraCodigo>>(palabrascodigo));
        
            return resultado;
        }

        [ChildActionOnly]
        public PartialViewResult _Comentarios(string idArticulo)
        {
            // Recuperación de comentarios asociados a "articulo id".
            ViewBag.TopeComentarios = ConfigurationManager.AppSettings["NumeroComentAntiguos"];
            return PartialView(new List<centro.recursos.net.Models.Entity_Framework.Comentario>());
        }

        public ActionResult DescargarCodigo(string archivo, int indice, string carpeta)
        {
            // carpeta = MVC
            // archivo = program.cs
            // indice = 1
            string[] infoArchivo = archivo.Split('.');
            string nombreArchivo = $"{indice.ToString("D2")}@{infoArchivo[0]}@{infoArchivo[1]}.txt";
            string rutaArchivo = $"{Server.MapPath("~")}{Rutas.RutaCodigoArticulos}/{carpeta}/{nombreArchivo}";
            var Resultado = GeneraArchivoRespuesta(rutaArchivo, archivo);

            return Resultado;
        }
    }
}
