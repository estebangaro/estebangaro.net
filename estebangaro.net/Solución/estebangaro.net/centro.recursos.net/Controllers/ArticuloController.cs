using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Utileria;
using System;
using System.Collections.Generic;
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

        public PartialViewResult _ConvertidorCodigo(string rutaCodigos, 
            string idArticulo)
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
    }
}
