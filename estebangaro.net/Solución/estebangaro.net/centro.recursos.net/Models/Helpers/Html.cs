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
            else
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
                        new XElement("img", new XAttribute("src", "../../imagenes/flecha.png"))) : 
                        new XElement("span", opcion.Descripcion),
                    new XElement("ul", _elementosHijo));
            }
            else
                _elemento = new XElement( _esSuperMenu ? "nav" : "li",
                    new XElement("span",
                        new XElement("a",
                            new XText(opcion.Descripcion),
                            new XAttribute("href", opcion.URI))));

            return _elemento;
        }
    }
}