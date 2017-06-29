using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Entity_Framework
{
    public class OpcionMenu
    {
        public short OpcionMenuId { get; set; }
        public string Descripcion { get; set; }
        public short Orden { get; set; }
        public string URI { get; set; }
        public bool Visible { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public Nullable<int> MenuPadre { get; set; }
        public virtual OpcionMenu Padre { get; set; }
        public virtual ICollection<OpcionMenu> Opciones { get; set; }
    }
}