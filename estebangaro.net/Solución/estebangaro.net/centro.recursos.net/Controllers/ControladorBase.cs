using centro.recursos.net.Models.Utileria;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace centro.recursos.net.Controllers
{
    public class ControladorBase : Controller
    {
        #region Propiedades

        protected Models.Repositorios.IGaroNetDb Repositorio { get; set; }
        private string CadenaConexion
        {
            get
            {
                string amb = ConfigurationManager.AppSettings["ambiente"];
                amb = string.IsNullOrEmpty(amb) ? "dev" : amb;
                return $"GaroNETDbContexto_{amb}";
            }
        }

        #endregion

        #region Controladores

        public ControladorBase()
        {
            Repositorio = new Models.Repositorios.GaroNetDb(CadenaConexion);
        }

        public ControladorBase(Models.Repositorios.IGaroNetDb repositorio)
        {
            Repositorio = repositorio;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Genera una cadena con la información de excepción en el orden "Detalle|Excepcion|Icono(int)".
        /// </summary>
        /// <typeparam name="E">Tipo concreto de resultado de la respuesta.</typeparam>
        /// <param name="respuesta">Representa la respuesta de invocación de un método del repositorio.</param>
        /// <returns>con la información de excepción.</returns>
        protected string GeneraRespuestaExcepcion<E>(Respuesta<E> respuesta)
        {
            return $"{respuesta.Detalle}|{respuesta.Excepcion}|{(int)respuesta.Icono}";
        }

        #endregion
    }
}