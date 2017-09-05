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
            //HabilitaBD();
            //CargaDatos();
            return View("Inicio");
        }

        public PartialViewResult _Menu()
        {
            Respuesta<List<OpcionMenu>> opciones;

            if (Session["_HTMLmenu"] == null)
                opciones = Repositorio.ObtenOpcionesMenu();
            else
                opciones = null;
            PartialViewResult resultado = null;
            if (opciones != null && opciones.Estado)
                resultado = PartialView(opciones.Resultado);
            else if (opciones != null)
                ViewBag.LayoutExcepcion += (ViewBag.LayoutExcepcion != null ?
                    $"@{GeneraRespuestaExcepcion<List<OpcionMenu>>(opciones)}" :
                    GeneraRespuestaExcepcion<List<OpcionMenu>>(opciones));
            else
                resultado = PartialView(null);

            return resultado;
        }

        public PartialViewResult _Carrusel()
        {
            Respuesta<List<AvisoCarrusel>> avisos;

            if (Session["_HTMLavisoscarrusel"] == null)
                avisos = Repositorio.ObtenAvisosCarrusel();
            else
                avisos = null;
            PartialViewResult resultado = null;

            if (avisos != null && avisos.Estado)
                resultado = PartialView(avisos.Resultado);
            else if (avisos != null)
                ViewBag.LayoutExcepcion += (ViewBag.LayoutExcepcion != null ?
                    $"@{GeneraRespuestaExcepcion<List<AvisoCarrusel>>(avisos)}" :
                    GeneraRespuestaExcepcion<List<AvisoCarrusel>>(avisos));
            else
            {
                resultado = PartialView(null);
                ViewBag.AvisosCuenta = ((Tuple<MvcHtmlString, int>)Session["_HTMLavisoscarrusel"]).Item2;
            }

            return resultado;
        }

        public PartialViewResult _NoticiasP()
        {
            Respuesta<List<NoticiaPrincipal>> noticias = Repositorio.ObtenNoticias();
            PartialViewResult resultado = null;
            if (noticias.Estado)
                resultado = PartialView(noticias.Resultado);
            else
                ViewBag.LayoutExcepcion += (ViewBag.LayoutExcepcion != null ?
                    $"@{GeneraRespuestaExcepcion<List<NoticiaPrincipal>>(noticias)}" :
                    GeneraRespuestaExcepcion<List<NoticiaPrincipal>>(noticias));

            return resultado;
        }

        public PartialViewResult _SeccionesMultimedia()
        {
            Respuesta<List<Multimedia>> multimedia = Repositorio.ObtenMultimedia();
            PartialViewResult resultado = null;
            if (multimedia.Estado)
                resultado = PartialView(multimedia.Resultado);
            else
                ViewBag.LayoutExcepcion += (ViewBag.LayoutExcepcion != null ?
                    $"@{GeneraRespuestaExcepcion<List<Multimedia>>(multimedia)}" :
                    GeneraRespuestaExcepcion<List<Multimedia>>(multimedia));

            return resultado;
        }

        public PartialViewResult _SeccionesAutor()
        {
            Respuesta<List<Autor>> autores = Repositorio.ObtenAutores();
            PartialViewResult resultado = null;
            if (autores.Estado)
                resultado = PartialView(autores.Resultado);
            else
                ViewBag.LayoutExcepcion += (ViewBag.LayoutExcepcion != null ?
                    $"@{GeneraRespuestaExcepcion<List<Autor>>(autores)}" :
                    GeneraRespuestaExcepcion<List<Autor>>(autores));

            return resultado;
        }
    }
}