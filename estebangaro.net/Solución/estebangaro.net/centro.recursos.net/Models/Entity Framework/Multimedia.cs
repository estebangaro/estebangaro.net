using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class Multimedia
    {
        public int MultimediaId { get; set; }
        public string Titulo { get; set; }
        public string Informacion { get; set; }
        public string Imagen { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public string URI { get; set; }
        public virtual Articulo Articulo { get; set; }
        public int BotonId { get; set; }
        public virtual BotonAviso Boton { get; set; }
    }
}