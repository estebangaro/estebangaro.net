using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ApellidoM { get; set; }
        public DateTime Nacimiento { get; set; }
        public InfoRegistro Auditoria { get; set; }
        public string Imagen { get; set; }
        public bool Estado { get; set; }
        public int Orden { get; set; }

        public int IdPuesto { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}