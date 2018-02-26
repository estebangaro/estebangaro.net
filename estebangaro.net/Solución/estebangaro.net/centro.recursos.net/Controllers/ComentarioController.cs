using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Repositorios;
using centro.recursos.net.Models.Utileria;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace centro.recursos.net.Controllers
{
    public class ComentarioController : ApiBase
    {
        public HttpResponseMessage GetComentarios(string id, string tipo, int ultimoantiguo)
        {
            System.Threading.Thread.Sleep(2000);
            string Url = $"/{id.Replace("-", "/")}";
            var Consulta = tipo == "recientes" ? Repositorio.ObtenComentarios(Url
                , numeroComentarios: ConfiguracionesApp.NumeroComentariosInicial) :
                Repositorio.ObtenComentarios(Url, ultimoantiguo, COMENTARIOS.ANTIGUOS);

            return Consulta.Estado ? Request.CreateResponse(HttpStatusCode.OK,
                    new { Comentarios = Consulta.Resultado.Item1, Cuenta = Consulta.Resultado.Item2 }) :
                Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage PostComentario(Comentario comentario)
        {
            Respuesta<Tuple<List<Comentario>, int>> GuardaComentarioEstado =
                Repositorio.GuardaComentario(comentario);

            return GuardaComentarioEstado.Estado ? Request.CreateResponse(HttpStatusCode.Created,
                new
                {
                    Comentarios = GuardaComentarioEstado.Resultado.Item1,
                    Cuenta = GuardaComentarioEstado.Resultado.Item2
                }) :
                Request.CreateResponse(HttpStatusCode.Conflict);
        }

        [AcceptVerbs("GaroComentariosAcercaD")]
        public HttpResponseMessage GuardaComentarioAcercaDe(ComentarioAcercaD comentario)
        {
            Respuesta<int> GuardaComentarioEstado =
                Repositorio.GuardaComentario(comentario);

            return GuardaComentarioEstado.Estado ? Request.CreateResponse(HttpStatusCode.Created) :
                Request.CreateResponse(HttpStatusCode.Conflict);
        }
    }
}
