using System.Collections.Generic;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class CategoriaPalabraCodigo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public virtual ICollection<PalabraCodigo> Palabras { get; set; }
    }
}