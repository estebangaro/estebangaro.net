using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public Nullable<int> IdComentarioP { get; set; }
        public virtual Comentario ComentarioPadre { get; set; }
        public string URI { get; set; }
        public virtual Articulo Articulo { get; set; }
        public string Email { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}