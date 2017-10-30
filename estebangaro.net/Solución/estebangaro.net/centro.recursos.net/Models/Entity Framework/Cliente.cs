using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class Cliente
    {
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Avatar { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}