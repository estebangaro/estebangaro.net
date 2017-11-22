﻿using System;
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
        public static string RutaImagenesVisualizadorCodigo { get; } = ConfigurationManager.AppSettings["ImagenesVisualizadorCodigo"];
        public static string RutaImagenesMenu { get; } = ConfigurationManager.AppSettings["ImagenesMenu"];
        public static string RutaImagenesNoticias { get; } = ConfigurationManager.AppSettings["ImagenesNoticias"];
        public static string RutaCodigoArticulos { get; } = ConfigurationManager.AppSettings["codigoArticulos"];
        public static string RutaPlantillasHtml { get; } = ConfigurationManager.AppSettings["PlantillasHtml"];
    }

    public static class ConfiguracionesApp
    {
        public static int NumeroComentariosAntiguos { get; } = int.Parse(ConfigurationManager.AppSettings["NumeroComentAntiguos"] ?? "0");
        public static int NumeroComentariosInicial { get; } = int.Parse(ConfigurationManager.AppSettings["NumeroComentInicial"] ?? "0");
    }
}