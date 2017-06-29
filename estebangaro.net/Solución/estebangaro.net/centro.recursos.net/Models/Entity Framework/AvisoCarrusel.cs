using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class AvisoCarrusel
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public string URI { get; set; }
        public bool Visible { get; set; }
        public short Orden { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public int BotonId { get; set; }
        public virtual BotonAviso Boton { get; set; }
    }
}