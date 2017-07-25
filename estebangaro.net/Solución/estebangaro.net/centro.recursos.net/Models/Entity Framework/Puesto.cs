using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class Puesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Inicio { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public virtual ICollection<Autor> Autores { get; set; }
    }
}