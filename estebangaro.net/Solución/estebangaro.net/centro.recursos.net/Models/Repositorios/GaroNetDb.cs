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

        public Respuesta<Articulo> ObtenInfoArticulo(string articuloId)
        {
            Respuesta<Articulo> estado;

            try
            {
                Articulo articuloInformacion = dbContextoEF.Articulos
                    .Include(articulo => articulo.Noticias)
                    .FirstOrDefault(articulo => articulo.URI.ToLower() == articuloId);

                if(articuloInformacion != null)
                    estado = Respuesta<object>.GeneraRespuestaNoExcepcion<Articulo>(true, articuloInformacion);
                else
                    estado = Respuesta<object>.
                        GeneraRespuestaNoExcepcion<Articulo>(false, null,
                        detalle: "Tenemos problemas para recuperar la información de artículo, intentalo mas tarde",
                        iconoCliente: ICONOS_RESPUESTA.ADVERTENCIA);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<Articulo>(ex,
                    NombreMetodo: "GaroNetDb.ObtenInfoArticulo(string)");
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

                if (palabras.Count > 0)
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

        public Respuesta<List<ClasePersonalizada>> ObtenClasesPersonalizadasCodigo(string articuloId = null)
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<List<ClasePersonalizada>> estado;

            try
            {
                var clases = articuloId != null ? dbContextoEF
                    .ClasesPersonalizadasCode
                    .Where(clasep => clasep.ArticuloId == articuloId)
                    .ToList() :
                    new List<ClasePersonalizada>();

                estado = Respuesta<object>.GeneraRespuestaNoExcepcion<List<ClasePersonalizada>>(true, clases);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<List<ClasePersonalizada>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenClasesPersonalizadasCodigo(int?)");
            }

            return estado;
        }

        public Respuesta<Tuple<List<Comentario>, int>> GuardaComentario(Comentario comentario)
        {
            Respuesta<Tuple<List<Comentario>, int>> estado;

            try
            {
                string[] InfoCliente = comentario.Email.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                comentario.Email = InfoCliente[0];
                if (InfoCliente[1] == "0")
                    dbContextoEF.ClientesArticulos.Add(new Cliente
                    {
                        Email = InfoCliente[0],
                        Avatar = InfoCliente[2],
                        Auditoria = new InfoRegistro { UsuarioCreacion = "WEB API"},
                        Nombre = comentario.Auditoria.UsuarioCreacion
                    });
                dbContextoEF.Comentarios.Add(comentario);
                dbContextoEF.SaveChanges();

                estado = ObtenComentarios(comentario.URI, int.Parse(InfoCliente[3]),
                    idComentarioPadre: comentario.IdComentarioP);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<Tuple<List<Comentario>, int>>(ex,
                    NombreMetodo: "GaroNetDb.GuardaComentario(Comentario)");
            }

            return estado;
        }

        public Respuesta<Tuple<List<Comentario>, int>> ObtenComentarios(string articulo, int idComentarioUltimoReciente = 0,
            COMENTARIOS tipo = COMENTARIOS.RECIENTES, int? idComentarioPadre = null, int? numeroComentarios = null)
        {
            // Publicación de comentarios; registro de comentarios NIVEL 0. (Comentarios.Recientes, 
            //  Comentario reciente = 0 ó > 0, idComentarioPadre = null y numeroComentarios = null).
            // Respuesta de comentarios; registro de comentarios NIVEL 1,2. (Comentarios.Recientes, 
            //  Comentario reciente = 0 ó > 0, idComentarioPadre > 0 y numeroComentarios = null).
            // Obtención de comentarios antiguos; operación "Mostrar Más". (Comentarios.Antiguos, Comentario último > 0,
            //  idComentarioPadre = null y numeroComentarios = null).
            // Carga incial de coentarios. (Comentarios.Recientes, Comentario reciente = 0, idComentarioPadre = null y 
            //  numeroComentarios > 0)
            List<Comentario> comentarios;
            IQueryable<Comentario> consulta;
            int cuenta;
            Respuesta<Tuple<List<Comentario>, int>> estado;
            try
            {
                consulta = from comentario in dbContextoEF.Comentarios
                              .Include(coment => coment.Cliente).Include(coment => coment.Comentarios)
                           where articulo == comentario.URI.ToLower() && comentario.IdComentarioP == idComentarioPadre
                              && (tipo == COMENTARIOS.RECIENTES ? comentario.Id > idComentarioUltimoReciente :
                                 comentario.Id < idComentarioUltimoReciente)
                           select comentario;

                comentarios = tipo == COMENTARIOS.ANTIGUOS ? consulta
                    .OrderByDescending(coment => coment.Id)
                    .Take(ConfiguracionesApp.NumeroComentariosAntiguos)
                    .ToList():
                    tipo == COMENTARIOS.RECIENTES ? !numeroComentarios.HasValue ? consulta.ToList() :
                        consulta.OrderByDescending(coment => coment.Id).Take(numeroComentarios.Value).ToList() : null;
                if (comentarios == null)
                    throw new NotImplementedException("No se tiene soporte para el tipo de comentario especificado");

                //cuenta = tipo == COMENTARIOS.ANTIGUOS ? consulta.Count() : comentarios.Count;
                cuenta = consulta.Count();

                for (int i = 0; i < comentarios.Count; i++)
                {
                    Comentario ComentarioProxy = comentarios[i];
                    comentarios[i] = RevierteYPreparaProxyComentario(ComentarioProxy);
                }

                estado = Respuesta<object>.GeneraRespuestaNoExcepcion<Tuple<List<Comentario>, int>>(true, 
                    new Tuple<List<Comentario>, int>(comentarios, cuenta));
            }
            catch(Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<Tuple<List<Comentario>, int>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenComentarios(string, int = 0,COMENTARIOS = COMENTARIOS.RECIENTES, " +
                        "int? = null, int?  = null)");
            }
            finally
            {
                dbContextoEF.Configuration.ProxyCreationEnabled = true;
            }

            return estado;
        }

        private Comentario RevierteYPreparaProxyComentario(Comentario comentario)
        {
            if (comentario.Comentarios != null && comentario.Comentarios.Count > 0)
            {
                comentario.Comentarios = comentario.Comentarios.ToList();
                for (int i = 0; i < comentario.Comentarios.Count; i++)
                {
                    Comentario ComentarioProxy = ((List<Comentario>)(comentario.Comentarios))[i];
                    ((List<Comentario>)(comentario.Comentarios))[i] = RevierteYPreparaProxyComentario(ComentarioProxy);
                }
            }

            return new Comentario
            {
                Articulo = comentario.Articulo,
                Auditoria = comentario.Auditoria,
                Cliente = new Cliente
                {
                    Auditoria = comentario.Cliente.Auditoria,
                    Avatar = comentario.Cliente.Avatar,
                    Nombre = comentario.Cliente.Nombre,
                    Email = comentario.Cliente.Email,
                    Comentarios = null
                },
                ComentarioPadre = null,
                Comentarios = comentario.Comentarios,
                Contenido = comentario.Contenido,
                Email = comentario.Email,
                Id = comentario.Id,
                IdComentarioP = comentario.IdComentarioP,
                URI = comentario.URI
            };
        }

        public Respuesta<Cliente> ObtenCliente(string email)
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<Cliente> estado;

            try
            {
                var cliente = dbContextoEF.ClientesArticulos
                    .FirstOrDefault(client => client.Email == email);

                estado = Respuesta<object>.GeneraRespuestaNoExcepcion<Cliente>(true, cliente);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<Cliente>(ex,
                    NombreMetodo: "GaroNetDb.ObtenCliente(string)");
            }

            return estado;
        }

        public Respuesta<Autor> ObtenAutor(int id)
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<Autor> estado;

            try
            {
                var autor = dbContextoEF.Autores
                    .Include(_autor => _autor.Puesto)
                    .Include(_autor => _autor.RedesSociales)
                    .FirstOrDefault(_autor => _autor.Id == id);

                estado = Respuesta<object>.GeneraRespuestaNoExcepcion<Autor>(true,
                    LimpiaYPreparaAutorEnDependencias(autor));
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<Autor>(ex,
                    NombreMetodo: "GaroNetDb.ObtenAutor(id)");
            }

            return estado;
        }

        private Autor LimpiaYPreparaAutorEnDependencias(Autor autor)
        {
            if (autor != null)
            {
                if (autor.Puesto != null)
                {
                    autor.Puesto.Autores = null;
                    autor.Puesto.Auditoria = null;
                }
                if (autor.RedesSociales != null)
                {
                    foreach (RedSocial red in autor.RedesSociales)
                    {
                        red.Auditoria = null;
                        red.Autor = null;
                    }
                    autor.RedesSociales = autor.RedesSociales.Where(red => red.Visible).ToList();
                }
            }

            return autor;
        }

        public Respuesta<List<ArticuloBusqueda>> ObtenArticulos(string busqueda)
        {
            dbContextoEF.Configuration.ProxyCreationEnabled = false;
            Respuesta<List<ArticuloBusqueda>> estado;
            List<string> tags = busqueda.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(tag => tag.Trim().ToLower()).ToList();

            try
            {
                var ArticuloBusquedas = dbContextoEF.Tags
                    .Include(etiqueta => etiqueta.Articulos)
                    .Where(etiqueta => tags.Contains(etiqueta.Etiqueta.ToLower()))
                    .SelectMany(etiqueta => etiqueta.Articulos)
                    .Distinct()
                    .Include(articulo => articulo.Noticias)
                    .AsEnumerable()
                    .Select(articulo => new ArticuloBusqueda
                    {
                        Descripcion = articulo.Noticias.Last().Descripcion,
                        Titulo = articulo.Titulo,
                        URI = articulo.URI
                    }).ToList();

                estado = Respuesta<object>.GeneraRespuestaNoExcepcion<List<ArticuloBusqueda>>(true,
                    ArticuloBusquedas);
            }
            catch (Exception ex)
            {
                estado = Respuesta<object>.
                    GeneraRespuestaExcepcion<List<ArticuloBusqueda>>(ex,
                    NombreMetodo: "GaroNetDb.ObtenArticuloBusquedas(string)");
            }

            return estado;
        }

        #endregion
    }
}