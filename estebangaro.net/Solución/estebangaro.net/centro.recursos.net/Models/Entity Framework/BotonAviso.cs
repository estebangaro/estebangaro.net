using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class BotonAviso
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public string Color { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public virtual ICollection<AvisoCarrusel> Avisos { get; set; }
        public virtual ICollection<Multimedia> Multimedia { get; set; }
    }
}