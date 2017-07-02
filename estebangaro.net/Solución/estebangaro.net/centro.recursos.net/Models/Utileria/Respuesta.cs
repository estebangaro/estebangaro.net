using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Utileria
{
    public class Respuesta<E>
    {
        #region Propiedades

        public bool Estado { get; set; }
        public ICONOS_RESPUESTA Icono { get; set; }
        public string Detalle { get; set; }
        public string Excepcion { get; set; }
        public E Resultado { get; set; }

        #endregion

        #region Métodos

        public static Respuesta<S> GeneraRespuestaExcepcion<S>(
            Exception ex, string NombreMetodo = null,
            string detalle = null)
        {
            string msjGenerico = ConfigurationManager.AppSettings["RespErrorGenerico"];
            msjGenerico = string.IsNullOrEmpty(msjGenerico) ? "Ups algo salió mal, intentalo mas tarde" : msjGenerico;

            return new Respuesta<S>
            {
                Estado = false,
                Detalle = string.IsNullOrEmpty(detalle) ?
                    string.IsNullOrEmpty(NombreMetodo) ?
                        msjGenerico :
                        $"Ha fallado la ejecución de {NombreMetodo}" :
                    detalle,
                Excepcion = ex.Message,
                Icono = ICONOS_RESPUESTA.ERROR
            };
        }

        public static Respuesta<S> GeneraRespuestaNoExcepcion<S>(
                bool estado, S resultado, string detalle = null,
                ICONOS_RESPUESTA iconoCliente = ICONOS_RESPUESTA.OK
            )
        {
            return new Respuesta<S>
            {
                Estado = estado,
                Detalle = detalle,
                Excepcion = null,
                Icono = iconoCliente,
                Resultado = resultado
            };
        }

        #endregion
    }

    public enum ICONOS_RESPUESTA { OK, ADVERTENCIA, CONFIRMACION, ERROR};
}