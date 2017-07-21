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

        public Respuesta<Tuple<List<NoticiaPrincipal>, TIPOS_NOTICIASP>> ObtenNoticias()
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            List<NoticiaPrincipal> noticiasRecientes, noticiasComentadas;
            Respuesta<Tuple<List<NoticiaPrincipal>, TIPOS_NOTICIASP>> estado;
            TIPOS_NOTICIASP tipos;
 
            try
            {
                tipos = TIPOS_NOTICIASP.RECIENTES_MASCOMENTADAS;
                noticiasRecientes = dbContextoEF
                    .NoticiasPrincipales
                    .OrderByDescending(noticia => noticia.Auditoria.Creacion)
                    .Take(4)
                    .ToList();
                List<int> excluirNoticias = noticiasRecientes.Select(noti => noti.Id).ToList();
                noticiasComentadas = dbContextoEF
                    .NoticiasPrincipales
                    .Where(noti => !excluirNoticias.Contains(noti.Id))
                    //.OrderByDescending(noti => noti.Comentarios.Count())
                    .Take(4)
                    .ToList();
                if (noticiasComentadas.Count < 4)
                {
                    tipos = TIPOS_NOTICIASP.RECIENTES;
                    noticiasComentadas = dbContextoEF
                    .NoticiasPrincipales
                    .OrderByDescending(noticia => noticia.Auditoria.Creacion)
                    .Skip(4)
                    .Take(4)
                    .ToList();
                }
                var conjunto = noticiasRecientes.Union(noticiasComentadas).ToList();

                if (conjunto.Count == 8) // Recuperar de archivo de configuración.
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion(true,
                        new Tuple<List<NoticiaPrincipal>, TIPOS_NOTICIASP>(conjunto, tipos));
                else
                    estado = Respuesta<object>.
                        GeneraRespuestaNoExcepcion<Tuple<List<NoticiaPrincipal>, TIPOS_NOTICIASP>>(false, null,
                        detalle: "Tenemos problemas para recuperar los noticias principales, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<Tuple<List<NoticiaPrincipal>, TIPOS_NOTICIASP>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenNoticias()");
            }

            return estado;
        }

        #endregion
    }
}