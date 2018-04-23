using centro.recursos.net.Models.Repositorios;
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
            using (IGaroNetDb repositorio = Repositorio)
            {
                var Consulta = repositorio.ObtenAutor(id);
                HttpResponseMessage Respuesta = Consulta.Estado ? Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        Estado = Consulta.Resultado != null,
                        Autor = Consulta.Resultado,
                        Edad = Consulta.Resultado != null ? Models.Utileria.Utileria.CalculaEdad(Consulta.Resultado.Nacimiento) :
                            0
                    }) :
                    Request.CreateResponse(HttpStatusCode.Conflict);

                return Respuesta;
            }
        }

        [Route("Articulo/Busqueda/{busqueda}")]
        [AcceptVerbs("GaroArticulos")]
        public HttpResponseMessage ObtenArticulosPorEtiquetas(string busqueda)
        {
            using (IGaroNetDb repositorio = Repositorio)
            {
                var Consulta = repositorio.ObtenArticulos(busqueda);
                HttpResponseMessage Respuesta = Consulta.Estado ? Request.CreateResponse(HttpStatusCode.OK,
                    Consulta.Resultado) : Request.CreateResponse(HttpStatusCode.Conflict);

                return Respuesta;
            }
        }
    }
}
