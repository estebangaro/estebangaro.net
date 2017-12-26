using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace centro.recursos.net.Controllers
{
    public class AutorController : ApiBase
    {
        [AcceptVerbs("GaroAutores")]
        // http://localhost/api/Autor/25 HTTP VERB: GaroAutores
        public HttpResponseMessage ObtenAutor(int id)
        {
            var Consulta = Repositorio.ObtenAutor(id);
            HttpResponseMessage Respuesta = Consulta.Estado ? Request.CreateResponse(HttpStatusCode.OK,
                new { Estado = Consulta.Resultado != null, Autor = Consulta.Resultado,
                    Edad = 26
                }) :
                Request.CreateResponse(HttpStatusCode.Conflict);

            return Respuesta;
        }

        private int CalculaEdad(DateTime nacimiento)
        {
            throw new NotImplementedException();
        }
    }
}
