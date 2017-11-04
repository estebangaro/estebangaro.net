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
    public class ComentarioController : ApiController
    {
        #region Propiedades

        protected Models.Repositorios.IGaroNetDb Repositorio { get; set; }
        public string CadenaConexion
        {
            get
            {
                string amb = ConfigurationManager.AppSettings["ambiente"];
                amb = string.IsNullOrEmpty(amb) ? "dev" : amb;
                return $"GaroNETDbContexto_{amb}";
            }
        }

        #endregion

        #region Contructores

        public ComentarioController()
        {
            Repositorio = new Models.Repositorios.GaroNetDb(CadenaConexion);
        }

        public ComentarioController(Models.Repositorios.IGaroNetDb repositorio)
        {
            Repositorio = repositorio;
        }

        #endregion

        public HttpResponseMessage GetComentarios(string id)
        {
            var Consulta = (Repositorio as GaroNetDb).ObtenComentarios($"/{id.Replace("-", "/")}"
                , numeroComentarios: ConfiguracionesApp.NumeroComentariosInicial);

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

        [Route("comentario/obtenerv2/all")]
        // http://192.168.0.8:2510/api/comentario GET/HTTP 1.1
        public HttpResponseMessage GetComentariosV2()
        {

            //var Comentarios = (Repositorio as GaroNetDb).ObtenComentarios(
            //    articulo: "/articulo/csharp/linq", idComentarioUltimoReciente: 8,
            //    tipo: COMENTARIOS.ANTIGUOS);

            var Comentarios = (Repositorio as GaroNetDb).ObtenComentarios(
                articulo: "/articulo/csharp/linq", idComentarioUltimoReciente: 0);

            //var Comentarios = (Repositorio as GaroNetDb).ObtenComentarios(
            //    articulo: "/articulo/csharp/linq", idComentarioUltimoReciente: 8, idComentarioPadre: 6);

            return Comentarios.Estado ? Request.CreateResponse(HttpStatusCode.OK,
                    new { Comentarios = Comentarios.Resultado.Item1, Cuenta = Comentarios.Resultado.Item2 }) :
                Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
