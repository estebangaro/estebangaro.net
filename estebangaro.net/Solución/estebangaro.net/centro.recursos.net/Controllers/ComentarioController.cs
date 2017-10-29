using centro.recursos.net.Models.Entity_Framework;
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

        public HttpResponseMessage PostComentario(Comentario comentario)
        {
            Respuesta<Comentario> GuardaComentarioEstado = Repositorio.GuardaComentario(comentario);

            return GuardaComentarioEstado.Estado ? Request.CreateResponse(HttpStatusCode.Created,
                GuardaComentarioEstado.Resultado) :
                Request.CreateResponse(HttpStatusCode.Conflict);
        }
    }
}
