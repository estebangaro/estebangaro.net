using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class InfoRegistro
    {
        public Nullable<DateTime> Creacion { get; set; }
        public Nullable<DateTime> Modificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}