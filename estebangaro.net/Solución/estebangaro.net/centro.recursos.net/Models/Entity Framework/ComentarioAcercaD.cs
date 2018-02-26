using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class ComentarioAcercaD
    {
        public int NumeroComentario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Ciudad { get; set; }
        public string Asuntomsj { get; set; }
        public string Contenidomsj { get; set; }
        public InfoRegistro Auditoria { get; set; }
    }
}