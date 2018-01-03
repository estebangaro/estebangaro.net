using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Utileria
{
    public class ArticuloBusqueda
    {
        public string URI { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }

    public class ArticuloInfo
    {
        public Entity_Framework.Articulo Articulo { get; set; }
        public List<Entity_Framework.Articulo> ArticulosRelacionados { get; set; }
    }
}