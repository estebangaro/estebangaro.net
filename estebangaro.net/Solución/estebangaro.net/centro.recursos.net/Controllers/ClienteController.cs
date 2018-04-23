using centro.recursos.net.Models.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace centro.recursos.net.Controllers
{
    public class ClienteController : ApiBase
    {
        [AcceptVerbs("GaroClientes")]
        public HttpResponseMessage ObtenCliente(string id)
        {
            using (IGaroNetDb repositorio = Repositorio)
            {
                var Consulta = repositorio.ObtenCliente(id);
                HttpResponseMessage Respuesta = Consulta.Estado ? Request.CreateResponse(HttpStatusCode.OK,
                    new { Estado = Consulta.Resultado != null, Cliente = Consulta.Resultado }) :
                    Request.CreateResponse(HttpStatusCode.Conflict);

                return Respuesta;
            }
        }
    }
}
