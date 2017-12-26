using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class Tag
    {
        public string Etiqueta { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}