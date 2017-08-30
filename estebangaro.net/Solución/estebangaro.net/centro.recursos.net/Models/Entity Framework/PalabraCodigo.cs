using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class PalabraCodigo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public int CategoriaId { get; set; }
        public virtual CategoriaPalabraCodigo Categoria { get; set; }
    }
}