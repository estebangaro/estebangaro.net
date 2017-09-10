using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class Articulo
    {
        public string URI { get; set; }
        public string Titulo { get; set; }
        public string Localizacion { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public virtual ICollection<Autor> Autores { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<AvisoCarrusel> Avisos { get; set; }
        public virtual ICollection<NoticiaPrincipal> Noticias { get; set; }
        public virtual ICollection<OpcionMenu> OpcionesMenu { get; set; }
        public virtual ICollection<Multimedia> Multimedia { get; set; }
        public virtual ICollection<ClasePersonalizada> Clases { get; set; }
    }
}