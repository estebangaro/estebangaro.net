using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Utileria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace centro.recursos.net.Models.Repositorios
{
    public interface IGaroNetDb: IDisposable
    {
        Respuesta<List<OpcionMenu>> ObtenOpcionesMenu();

        Respuesta<List<AvisoCarrusel>> ObtenAvisosCarrusel();

        Respuesta<List<NoticiaPrincipal>> ObtenNoticias();

        Respuesta<Articulo> ObtenInfoArticulo(string articuloId);

        Respuesta<List<Articulo>> ObtenArticulosRelacionados(string articuloId);

        Respuesta<List<Multimedia>> ObtenMultimedia();

        Respuesta<List<Autor>> ObtenAutores();

        Respuesta<List<PalabraCodigo>> ObtenPalabrasCodigo();

        Respuesta<List<ClasePersonalizada>> ObtenClasesPersonalizadasCodigo(string articuloId = null);

        Respuesta<Tuple<List<Comentario>, int>> GuardaComentario(Comentario comentario);

        Respuesta<int> GuardaComentario(ComentarioAcercaD comentario);

        Respuesta<Tuple<List<Comentario>, int>> ObtenComentarios(string articulo, int idComentarioUltimoReciente = 0,
            COMENTARIOS tipo = COMENTARIOS.RECIENTES, int? idComentarioPadre = null, int? numeroComentarios = null);

        Respuesta<Cliente> ObtenCliente(string email);

        Respuesta<Autor> ObtenAutor(int id);

        Respuesta<List<ArticuloBusqueda>> ObtenArticulos(string busqueda);
    }
}
