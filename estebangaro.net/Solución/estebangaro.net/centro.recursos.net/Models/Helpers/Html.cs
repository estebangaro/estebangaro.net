using centro.recursos.net.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using centro.recursos.net.Models.Utileria;
using System.Text.RegularExpressions;
using System.Drawing;

namespace centro.recursos.net.Models.Helpers
{
    public static class Html
    {
        public static string CadenaConexion
        {
            get
            {
                string amb = ConfigurationManager.AppSettings["ambiente"];
                amb = string.IsNullOrEmpty(amb) ? "dev" : amb;
                return $"GaroNETDbContexto_{amb}";
            }
        }

        public static MvcHtmlString GeneraMenu(this HtmlHelper helper, List<OpcionMenu> opciones)
        {
            if (opciones != null)
            {
                using (Repositorios.IGaroNetDb Repositorio = 
                    new Models.Repositorios.GaroNetDb(CadenaConexion))
                {
                    var Consulta = Repositorio.ObtenOpcionesMenu();
                    opciones = Consulta.Resultado;

                    if (Consulta.Estado)
                    {
                        List<XElement> _opciones = new List<XElement>();
                        foreach (OpcionMenu opcion in opciones)
                            _opciones.Add(ObtenMarcado(opcion));

                        MvcHtmlString _menu = new MvcHtmlString(new XElement("div", _opciones).ToString());
                        helper.ViewContext.RequestContext.HttpContext.Session["_HTMLmenu"] = _menu;

                        return _menu;
                    }
                }
            }
            return helper.ViewContext.RequestContext.
                HttpContext.Session["_HTMLmenu"] as MvcHtmlString;
        }

        private static XElement ObtenMarcado(OpcionMenu opcion)
        {
            XElement _elemento;
            bool _esSuperMenu = !opcion.MenuPadre.HasValue;
            List<OpcionMenu> hijos = opcion.Opciones.Where(op => op.Visible).OrderBy(op => op.Orden).ToList();
            if (hijos.Count > 0)
            {
                List<XElement> _elementosHijo = new List<XElement>();
                foreach (OpcionMenu opcionH in hijos)
                {
                    _elementosHijo.Add(ObtenMarcado(opcionH));
                }
                _elemento = new XElement(_esSuperMenu ? "nav" : "li",
                    _esSuperMenu ? new XElement("span", opcion.Descripcion,
                        new XElement("img", new XAttribute("src", $"{Rutas.ImagenesMenu}/flecha.png"))) :
                        new XElement("span", opcion.Descripcion),
                    new XElement("ul", _elementosHijo));
            }
            else
                _elemento = new XElement(_esSuperMenu ? "nav" : "li",
                    new XElement("span",
                        !string.IsNullOrEmpty(opcion.URI) ?
                        new XElement("a",
                            new XText(opcion.Descripcion),
                            new XAttribute("href", opcion.URI)) :
                        (XNode)new XText(opcion.Descripcion)
                        ));

            return _elemento;
        }

        public static MvcHtmlString GeneraAvisos(this HtmlHelper helper, List<AvisoCarrusel> avisos)
        {
            string _avisos = "";

            if (avisos != null)
            {
                foreach (AvisoCarrusel aviso in avisos)
                    _avisos += ObtenMarcado(aviso).ToString();

                MvcHtmlString avisosHTML = new MvcHtmlString(_avisos);
                helper.ViewContext.RequestContext.HttpContext.Session["_HTMLavisoscarrusel"] =
                    new Tuple<MvcHtmlString, int>(avisosHTML, avisos.Count);

                return avisosHTML;
            }
            return ((Tuple<MvcHtmlString, int>)helper.ViewContext.RequestContext.
                HttpContext.Session["_HTMLavisoscarrusel"]).Item1;
        }

        private static XElement ObtenMarcado(AvisoCarrusel aviso)
        {
            return new XElement("div",
                new XElement("h1", aviso.Contenido),
                new XElement("a", aviso.Boton.Texto,
                    new XAttribute("href", aviso.URI),
                    new XAttribute("class", $"{aviso.Boton.Color.ToLower()}_slider")));
        }

        public static MvcHtmlString GeneraNoticias(this HtmlHelper helper, List<NoticiaPrincipal> noticias)
        {
            string _noticias = "";

            foreach (NoticiaPrincipal noticia in noticias)
                _noticias += ObtenMarcado(noticia).ToString();

            MvcHtmlString noticiasHTML = new MvcHtmlString(_noticias);

            return noticiasHTML;
        }

        private static XElement ObtenMarcado(NoticiaPrincipal noticia)
        {
            return new XElement("div",
                   new XElement("p",
                       new XText(noticia.Titulo),
                       new XAttribute("class", "tituloBNR")),
                   new XElement("p",
                       new XElement("img",
                           new XAttribute("src",
                                $"{Rutas.ImagenesNoticias}/{noticia.Imagen}")),
                       new XAttribute("class", "imagenBNR")),
                   new XElement("p",
                       new XElement("a",
                           noticia.Descripcion,
                           new XAttribute("href", noticia.URI))),
                   new XAttribute("class", "bloqueNR")
                );
        }

        public static MvcHtmlString GeneraHTMLDeCarpetaCodigo(this HtmlHelper helper, 
            string nombreCarpeta, List<PalabraCodigo> palabrasCodigo, 
            List<string> clasesPersonalizadas = null)
        {
            int indice = 1;
            string marcado = string.Empty, encabezados = string.Empty;
            string rutaCodigo = $"{helper.ViewContext.HttpContext.Server.MapPath("~")}{Rutas.CodigoArticulos}/{nombreCarpeta}";
            ConvertidorCodigoAHTML convertidor = 
                new ConvertidorCodigoAHTML(palabrasCodigo, clasesPersonalizadas);

            foreach(string archivo in Directory.EnumerateFiles(rutaCodigo))
            {
                encabezados += $"<div class=\"EnzdoCodigoFuente\" indice=\"{indice++}\">" +
                    $"{LectorArchivos.ObtenNombreCodigo(archivo)}</div>";

                string contenido = LectorArchivos.ObtenContenidoCadena(archivo);
                if (contenido != null)
                    marcado += $"<div class=\"ctdrCodigo\"><pre>{convertidor.ObtenHTML(contenido)}</pre></div>";
            }
            encabezados += $"<a><img src=" +
                $"\"{Rutas.ImagenesVisualizadorCodigo}/download_2.png\" title=\"Descargar\" class=\"descargarCodigo\"></img></a>";
            encabezados += $"<input type=\"hidden\" value=\"{nombreCarpeta}\"/>";
            encabezados = $"<div class=\"EnzdosCodigoFuente\">{encabezados}</div>";
            marcado = $"<div class=\"CdrCodigoFuente\">{marcado}</div>";

            return new MvcHtmlString(encabezados + marcado);
        }

        public static MvcHtmlString GeneraImagenesAvatarsComentarios(this HtmlHelper helper, 
            FileInfo[] imagenesAvatar)
        {
            XElement divCdrAvatars = new XElement("div");
            XElement[] htmlAvatars = imagenesAvatar
                .OrderBy(delegate (FileInfo infoArchivo)
                {
                    Image bitmapObj = new Bitmap(infoArchivo.FullName);
                    // ID = 270 => Metadato de título de imagen.
                    var tituloPropiedad = bitmapObj.PropertyItems.FirstOrDefault(prop => prop.Id == 270);
                    string titulo = tituloPropiedad != null ?
                        System.Text.ASCIIEncoding.ASCII.GetString(tituloPropiedad.Value) :
                        infoArchivo.Name;
              
                    return titulo;
                })
                .Select(infoImagen => $"{infoImagen.Name}")
                .Select(delegate (string nombreArchivoAvatar)
                {
                    XElement avatar = new XElement("img");
                    avatar.Add(new XAttribute("src", $"{Rutas.ImagenesAvatarsComentarios}/{nombreArchivoAvatar}"));
                    return new XElement("div", avatar);
                }).ToArray();
            divCdrAvatars.Add(htmlAvatars);

            return new MvcHtmlString(divCdrAvatars.ToString());
        }

        public static MvcHtmlString ObtenArticuloInformacion(this HtmlHelper helper, Articulo articulo)
        {
            string rutaPlantilla = $"{helper.ViewContext.HttpContext.Server.MapPath("~")}" +
                $"{Rutas.PlantillasHtml}/InfoArticulo.html";
            XElement infoArticuloHTML = XElement.Load(rutaPlantilla);

            EstableceValorElemento(infoArticuloHTML, "p", "class", "Titulo", articulo.Titulo);
            EstableceValorElemento(infoArticuloHTML, "p", "class", "Descripcion", articulo.Noticias.Last().Descripcion);
            EstableceValorElemento(infoArticuloHTML, "span", "class", "FechaPub",
                articulo.Auditoria.Creacion.Value.ToShortDateString());
            try
            {
                ObtenElemento(infoArticuloHTML, "div", "class", "Autor")
                    .AddFirst(ObtenMarcadoImagen(articulo.Autores.ToList()));
                ObtenElemento(infoArticuloHTML, "p", "class", "Nombre")
                    .Add(ObtenMarcado(articulo.Autores.ToList()));
            }
            catch { }

            return new MvcHtmlString(infoArticuloHTML.ToString());
        }

        private static bool EstableceValorElemento(XElement elemento,
            string nombreElemento, string nombreAtributo, 
            string valorAtributo, string valor, 
            string atributo = null)
        {
            bool estado;

            try
            {
                XElement elementoHTML = ObtenElemento(elemento, nombreElemento, nombreAtributo, valorAtributo);

                if (string.IsNullOrEmpty(atributo))
                    elementoHTML.Add(new XText(valor));
                else
                    elementoHTML.Add(new XAttribute(atributo, valor));
                estado = true;
            }
            catch
            {
                estado = false;
            }

            return estado;
        }

        private static XElement ObtenElemento(XElement elemento,
            string nombreElemento, string nombreAtributo,
            string valorAtributo)
        {
            return elemento.Descendants(nombreElemento)
                    .First(delegate (XElement Elemento)
                    {
                        XAttribute atributo = Elemento.Attribute(nombreAtributo);
                        return atributo != null ? atributo.Value == valorAtributo : false;
                    });
        }

        private static XElement[] ObtenMarcadoImagen(List<Autor> autores)
        {
            return autores
                .Select(autorEntidad => new XElement("img", 
                    new XAttribute("class", "Imagen"),
                    new XAttribute("src", $"{Rutas.ImagenesAutores}/{autorEntidad.Imagen}")))
                .ToArray();
        }

        private static XElement[] ObtenMarcado(List<Autor> autores)
        {
            int numAutores = autores.Count;
            return autores
                .Select(delegate (Autor autorEntidad, int numeroAutor)
                {
                    string nombreAutor = $"{autorEntidad.Nombre} {autorEntidad.Apellido}";
                    return new XElement("span",
                        new XText(numeroAutor == 0 ? nombreAutor :
                            (numeroAutor + 1) == numAutores ? $"y {nombreAutor}" :
                            $", {nombreAutor}"),
                        new XAttribute("id", autorEntidad.Id));
                }).ToArray();
        }

    }
}