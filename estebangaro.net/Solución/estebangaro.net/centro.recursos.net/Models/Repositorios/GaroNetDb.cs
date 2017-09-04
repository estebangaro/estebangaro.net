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

        public Respuesta<List<NoticiaPrincipal>> ObtenNoticias()
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<List<NoticiaPrincipal>> estado;
 
            try
            {
                IQueryable<NoticiaPrincipal> recientes = dbContextoEF.NoticiasPrincipales.
                    OrderByDescending(noti => noti.Auditoria.Creacion).
                    Take(4);
                IQueryable<NoticiaPrincipal> comentadas = dbContextoEF.NoticiasPrincipales.
                    OrderByDescending(noti => noti.Articulo.Comentarios.Count).
                    Except(recientes).
                    Take(4);
                var conjunto = recientes.Union(comentadas).ToList();
                if (conjunto.Count == 8) // Recuperar de archivo de configuración.
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion(true, conjunto);
                else
                    estado = Respuesta<object>.
                        GeneraRespuestaNoExcepcion<List<NoticiaPrincipal>>(false, null,
                        detalle: "Tenemos problemas para recuperar los noticias principales, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<List<NoticiaPrincipal>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenNoticias()");
            }

            return estado;
        }

        public Respuesta<List<Multimedia>> ObtenMultimedia()
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<List<Multimedia>> estado;

            try
            {
                var multimedia = dbContextoEF.Multimedia.
                    Include(mult => mult.Boton).
                    Where(mult => mult.Estado).
                    OrderBy(mult => mult.Orden).
                    ToList();

                if (multimedia.Count > 0) // Recuperar de archivo de configuración.
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion<List<Multimedia>>(true, multimedia);
                else
                    estado = Respuesta<object>.
                        GeneraRespuestaNoExcepcion<List<Multimedia>>(false, null,
                        detalle: "Tenemos problemas para recuperar la sección multimedia, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<List<Multimedia>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenMultimedia()");
            }

            return estado;
        }

        public Respuesta<List<Autor>> ObtenAutores()
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<List<Autor>> estado;

            try
            {
                var autores = dbContextoEF.Autores.
                    Include(aut => aut.Puesto).
                    Where(aut => aut.Estado).
                    OrderBy(aut => aut.Orden).
                    ToList();

                if (autores.Count > 0) // Recuperar de archivo de configuración.
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion<List<Autor>>(true, autores);
                else
                    estado = Respuesta<object>.
                        GeneraRespuestaNoExcepcion<List<Autor>>(false, null,
                        detalle: "Tenemos problemas para recuperar la sección autores, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<List<Autor>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenAutores()");
            }

            return estado;
        }

        public Respuesta<List<PalabraCodigo>> ObtenPalabrasCodigo()
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<List<PalabraCodigo>> estado;

            try
            {
                var palabras = dbContextoEF.PalabrasCode.
                    ToList();

                if (palabras.Count > 0) // Recuperar de archivo de configuración.
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion<List<PalabraCodigo>>(true, palabras);
                else
                    estado = Respuesta<object>.
                        GeneraRespuestaNoExcepcion<List<PalabraCodigo>>(false, null,
                        detalle: "Tenemos problemas para recuperar las palabras de código, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<List<PalabraCodigo>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenPalabrasCodigo()");
            }

            return estado;
        }

        #endregion
    }
}