using centro.recursos.net.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Utileria
{
    public class PalabrasCodigo
    {
        public string RutaCodigoFuente { get; set; }
        public List<PalabraCodigo> PalabrasReservadas { get; set; }
        public List<string> ClasesPersonalizadas { get; set; }
    }
}