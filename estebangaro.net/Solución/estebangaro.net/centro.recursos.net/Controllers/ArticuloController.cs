using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Repositorios;
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
            ViewBag.Title = id;

            using (IGaroNetDb repositorio = Repositorio)
            {
                Respuesta<Articulo> articuloInfo =
                    repositorio.ObtenInfoArticulo(Request.Path.ToLower());
                Respuesta<List<Articulo>> articulosRel =
                    repositorio.ObtenArticulosRelacionados(Request.Path.ToLower());

                if (articuloInfo.Estado && articulosRel.Estado)
                    return View(viewName: id, model: new ArticuloInfo
                    {
                        Articulo = articuloInfo.Resultado,
                        ArticulosRelacionados = articulosRel.Resultado
                    });
                else
                    return View(viewName: "Error", model: articuloInfo.Detalle);
            }
        }

        [ChildActionOnly]
        public PartialViewResult _ConvertidorCodigo(string rutaCodigos, 
            string idArticulo = null)
        {
            using (IGaroNetDb repositorio = Repositorio)
            {
                Respuesta<List<ClasePersonalizada>> clasesPersonalizadas =
                    repositorio.ObtenClasesPersonalizadasCodigo(idArticulo);
                Respuesta<List<PalabraCodigo>> palabrascodigo =
                    repositorio.ObtenPalabrasCodigo();
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

        public ActionResult DescargarCodigo(string archivo, int indice, string carpeta)
        {
            // carpeta = MVC
            // archivo = program.cs
            // indice = 1
            string[] infoArchivo = archivo.Split('.');
            string nombreArchivo = $"{indice.ToString("D2")}@{infoArchivo[0]}@{infoArchivo[1]}.txt";
            string rutaArchivo = $"{Server.MapPath("~")}{Rutas.CodigoArticulos}/{carpeta}/{nombreArchivo}";
            var Resultado = GeneraArchivoRespuesta(rutaArchivo, archivo);

            return Resultado;
        }

        [ChildActionOnly]
        public PartialViewResult _Comentarios()
        {
            // Recuperación de comentarios asociados a "articulo id".
            ViewBag.TopeComentarios = ConfiguracionesApp.NumeroComentariosAntiguos;
            ViewBag.RutaImagenesComentarios = Rutas.ImagenesComentarios;
            ViewBag.RutaImagenesAvatars = Rutas.ImagenesAvatarsComentarios;
            ViewBag.AnidamientoMaxComentarios = ConfiguracionesApp.NumeroComentariosAnidados;
            ViewBag.UnidadStorageClienteComentarios = ConfiguracionesApp.UnidadStorageClienteComentarios;
            ViewBag.TiempoStorageClienteComentarios = ConfiguracionesApp.TiempoStorageClienteComentarios;
            ViewBag.AlmacenamientoWeb = ConfiguracionesApp.TipoAlmacenamientoWeb == "sesion" ? 1 :
                ConfiguracionesApp.TipoAlmacenamientoWeb == "local" ? 0 : -1;

            return PartialView(new List<centro.recursos.net.Models.Entity_Framework.Comentario>());
        }

        [ChildActionOnly]
        public PartialViewResult _PopUpG()
        {
            string rutaAvatars = $"{Server.MapPath("~")}" +
                $"{Rutas.ImagenesAvatarsComentarios}";
            System.IO.DirectoryInfo carpetaAvatars = new System.IO.DirectoryInfo(rutaAvatars);
            System.IO.FileInfo[] imagenes = carpetaAvatars.GetFiles();
            ViewBag.NumeroAvatars = imagenes.Count();

            return PartialView(imagenes);
        }
    }
}
