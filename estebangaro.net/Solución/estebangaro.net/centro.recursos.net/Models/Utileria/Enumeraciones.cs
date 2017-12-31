using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Utileria
{
    public enum TIPOS_NOTICIASP { RECIENTES_MASCOMENTADAS, RECIENTES};

    public enum LENGUAJE_CODIGO { CSHARP, SQL};

    public enum COMENTARIOS { RECIENTES, ANTIGUOS };

    public static class Rutas
    {
        public static string ImagenesVisualizadorCodigo { get; } = ConfigurationManager.AppSettings["ImagenesVisualizadorCodigo"];
        public static string ImagenesMenu { get; } = ConfigurationManager.AppSettings["ImagenesMenu"];
        public static string ImagenesNoticias { get; } = ConfigurationManager.AppSettings["ImagenesNoticias"];
        public static string CodigoArticulos { get; } = ConfigurationManager.AppSettings["codigoArticulos"];
        public static string PlantillasHtml { get; } = ConfigurationManager.AppSettings["PlantillasHtml"];
        public static string ImagenesAvatarsComentarios { get; } = ConfigurationManager.AppSettings["ImagenesAvatarComentarios"];
        public static string ImagenesComentarios { get; } = ConfigurationManager.AppSettings["ImagenesComentarios"];
        public static string ImagenesAutores { get; } = ConfigurationManager.AppSettings["ImagenesAutores"];
    }

    public static class ConfiguracionesApp
    {
        public static int NumeroComentariosAntiguos { get; } = int.Parse(ConfigurationManager.AppSettings["NumeroComentAntiguos"] ?? "0");
        public static int NumeroComentariosInicial { get; } = int.Parse(ConfigurationManager.AppSettings["NumeroComentInicial"] ?? "0");
        public static int NumeroComentariosAnidados { get; } = int.Parse(ConfigurationManager.AppSettings["NumeroComentariosAnidados"] ?? "0");
        public static string UnidadStorageClienteComentarios { get; } = ConfigurationManager.AppSettings["UnidadStorageClienteComentarios"].ToLower();
        public static int TiempoStorageClienteComentarios { get; } = int.Parse(ConfigurationManager.AppSettings["TiempoStorageClienteComentarios"] ?? "0");
        public static string TipoAlmacenamientoWeb { get; } = ConfigurationManager.AppSettings["AlmacenamientoWeb"].ToLower();
    }

    public static class Utileria
    {
        public static int CalculaEdad(DateTime nacimiento)
        {
            DateTime dia = DateTime.Today;
            int anios = dia.Year - nacimiento.Year;

            return dia.Year > nacimiento.Year ?
                dia.Month < nacimiento.Month ? anios - 1 :
                dia.Month > nacimiento.Month || dia.Day >= nacimiento.Day ? anios :
                anios - 1 : 0;
        }
    }
}