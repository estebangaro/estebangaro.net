using centro.recursos.net.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace centro.recursos.net.Models.Helpers
{
    public static class Html
    {
        public static MvcHtmlString GeneraMenu(this HtmlHelper helper, List<OpcionMenu> opciones)
        {
            var _HTMLMenu = helper.ViewContext.RequestContext.HttpContext.Session["_HTMLmenu"];
            if (_HTMLMenu == null)
            {
                List<XElement> _opciones = new List<XElement>();
                foreach (OpcionMenu opcion in opciones)
                    _opciones.Add(ObtenMarcado(opcion));

                MvcHtmlString _menu = new MvcHtmlString(new XElement("div", _opciones).ToString());
                helper.ViewContext.RequestContext.HttpContext.Session["_HTMLmenu"] = _menu;

                return _menu;
            }
            return _HTMLMenu as MvcHtmlString;
        }

        private static XElement ObtenMarcado(OpcionMenu opcion)
        {
            XElement _elemento;
            bool _esSuperMenu = !opcion.MenuPadre.HasValue;
            if (opcion.Opciones.Count > 0)
            {
                List<XElement> _elementosHijo = new List<XElement>();
                foreach (OpcionMenu opcionH in opcion.Opciones)
                {
                    _elementosHijo.Add(ObtenMarcado(opcionH));
                }
                _elemento = new XElement(_esSuperMenu ? "nav" : "li",
                    _esSuperMenu ? new XElement("span", opcion.Descripcion,
                        new XElement("img", new XAttribute("src", "/Resources/imagenes/menu/flecha.png"))) :
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
            var _HTMLAvisos = helper.ViewContext.RequestContext.HttpContext.Session["_HTMLavisoscarrusel"];

            if (_HTMLAvisos == null)
            {
                foreach (AvisoCarrusel aviso in avisos)
                    _avisos += ObtenMarcado(aviso).ToString();

                MvcHtmlString avisosHTML = new MvcHtmlString(_avisos);
                helper.ViewContext.RequestContext.HttpContext.Session["_HTMLavisoscarrusel"] = avisosHTML;

                return avisosHTML;
            }
            return _HTMLAvisos as MvcHtmlString;
        }

        private static XElement ObtenMarcado(AvisoCarrusel aviso)
        {
            return new XElement("div",
                new XElement("h1", aviso.Contenido),
                new XElement("a", aviso.Boton.Texto,
                    new XAttribute("href", aviso.URI),
                    new XAttribute("class", $"{aviso.Boton.Color.ToLower()}_slider")));
        }
    }
}