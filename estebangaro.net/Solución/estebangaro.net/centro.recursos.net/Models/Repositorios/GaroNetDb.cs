using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Utileria;

namespace centro.recursos.net.Models.Repositorios
{
    public class GaroNetDb : IGaroNetDb
    {
        #region Propiedades

        private GaroNETDbContexto dbContextoEF;

        #endregion

        #region Constructores

        public GaroNetDb()
        {
            dbContextoEF = new GaroNETDbContexto();
        }

        public GaroNetDb(string cadenaConexion)
        {
            dbContextoEF = new GaroNETDbContexto(cadenaConexion);
        }

        #endregion

        #region Métodos

        public Respuesta<List<OpcionMenu>> ObtenOpcionesMenu()
        {
            //dbContextoEF.Configuration.ProxyCreationEnabled = false;
            List<OpcionMenu> opciones;
            Respuesta<List<OpcionMenu>> estado;

            try
            {
                opciones = dbContextoEF
                    .OpcionesMenu
                    .Include(opcion => opcion.Opciones)
                    .Where(opcion => opcion.MenuPadre == null && opcion.Visible)
                    .OrderByDescending(opcion => opcion.Orden)
                    .ToList();
                if (opciones.Count > 0)
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion( true, opciones);
                else
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion<List<OpcionMenu>>(false, null,
                        detalle: "Tenemos problemas para recuperar el menú, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.GeneraRespuestaExcepcion<List<OpcionMenu>>(ex, 
                    NombreMetodo: "GaroNetDb.ObtenOpcionesMenu()");
            }

            return estado;
        }

        public Respuesta<List<AvisoCarrusel>> ObtenAvisosCarrusel()
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            List<AvisoCarrusel> avisos;
            Respuesta<List<AvisoCarrusel>> estado;

            try
            {
                avisos = dbContextoEF
                    .AvisosCarrusel
                    .Include(aviso => aviso.Boton)
                    .Where(aviso => aviso.Visible)
                    .OrderByDescending(aviso => aviso.Orden)
                    .ToList();

                if (avisos.Count > 0)
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion(true, avisos);
                else
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion<List<AvisoCarrusel>>(false, null,
                        detalle: "Tenemos problemas para recuperar los avisos del carrusel, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.GeneraRespuestaExcepcion<List<AvisoCarrusel>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenAvisosCarrusel()");
            }

            return estado;

        }

        #endregion
    }
}