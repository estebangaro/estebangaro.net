using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Utileria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace centro.recursos.net.Models.Repositorios
{
    public interface IGaroNetDb
    {
        Respuesta<List<OpcionMenu>> ObtenOpcionesMenu();

        Respuesta<List<AvisoCarrusel>> ObtenAvisosCarrusel();

        Respuesta<List<NoticiaPrincipal>> ObtenNoticias();

        Respuesta<List<Multimedia>> ObtenMultimedia();

        Respuesta<List<Autor>> ObtenAutores();

        Respuesta<List<PalabraCodigo>> ObtenPalabrasCodigo();

        Respuesta<List<ClasePersonalizada>> ObtenClasesPersonalizadasCodigo(string articuloId = null);

        Respuesta<Comentario> GuardaComentario(Comentario comentario);
    }
}
