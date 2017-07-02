using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Utileria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace centro.recursos.net.Controllers
{
    public class HomeController : ControladorBase
    {
        // Home
        public ActionResult GaRoNET()
        {
            throw new NotImplementedException();
        }

        public PartialViewResult _Menu()
        {
            Respuesta<List<OpcionMenu>> opciones = Repositorio.ObtenOpcionesMenu();
            PartialViewResult resultado = null;
            if (opciones.Estado)
                resultado = PartialView(opciones.Resultado);
            else
                ViewBag.LayoutExcepcion = GeneraRespuestaExcepcion<List<OpcionMenu>>(opciones);

            return resultado;
        }

        public PartialViewResult _Carrusel()
        {
            Respuesta<List<AvisoCarrusel>> avisos = Repositorio.ObtenAvisosCarrusel();
            PartialViewResult resultado = null;
            if (avisos.Estado)
                resultado = PartialView(avisos.Resultado);
            else
                ViewBag.LayoutExcepcion = GeneraRespuestaExcepcion<List<AvisoCarrusel>>(avisos);

            return resultado;
        }
    }
}