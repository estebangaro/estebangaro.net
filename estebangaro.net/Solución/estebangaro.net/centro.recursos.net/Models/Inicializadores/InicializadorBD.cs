using centro.recursos.net.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Inicializadores
{
    public class InicializadorBD: 
        DropCreateDatabaseIfModelChanges<GaroNETDbContexto>
    {
        protected override void Seed(GaroNETDbContexto context)
        {
            CargaDatos(context);
        }

        public void CargaDatos(GaroNETDbContexto contexto)
        {
            Articulo[] articulos =
            {
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "Lenguage Integrated Query",
                    URI = "/APIs/LINQ", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "LINQ to Objects",
                    URI = "/APIs/LINQ 2 Objects", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "LINQ to XML",
                    URI = "/APIs/LINQ 2 XML", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "LINQ to Entities",
                    URI = "/APIs/LINQ 2 Entities", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "ADO .NET (clásico)", // [4]
                    URI = "/APIs/ADO NET", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "Entity Framework",
                    URI = "/Frameworks/Entity Framework", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "Entity Framework Model First Approach",
                    URI = "/Frameworks/Entity Framework MFA", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "Entity Framework Database First Approach",
                    URI = "/Frameworks/Entity Framework DFA", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "Entity Framework Code First Approach",
                    URI = "/Frameworks/Entity Framework CFA", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "Entity Framework Técnicas de Modelado",
                    URI = "/Frameworks/Entity Framework TM", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "ASP .NET", // [10]
                    URI = "/Frameworks/ASP NET", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "ASP .NET WEB FORMS", // [11]
                    URI = "/Frameworks/ASP NET WEB FORMS", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "ASP .NET MVC", // [12]
                    URI = "/Frameworks/ASP NET WEB MVC", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "ASP .NET WEB API", // [13]
                    URI = "/Frameworks/ASP NET WEB API", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "WINDOWS FORMS", // [14]
                    URI = "/Frameworks/Windows Forms", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}},
                new Articulo{ Localizacion = "Ciudad de México", Titulo = "Windows Presentation Foundation", // [15]
                    URI = "/Frameworks/WPF", Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"}}
            };
            ClasePersonalizada[] clasesPersonalizadas = 
            {
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "HomeControllerFake",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "ControladorBase",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "Respuesta",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "OpcionMenu",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "AvisoCarrusel",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "NoticiaPrincipal",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "Multimedia",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} },
                new ClasePersonalizada{ Articulo = articulos[5], Nombre = "Autor",
                    Auditoria = new InfoRegistro{ UsuarioCreacion = "estebangaro"} }
            };
            Autor[] autores = 
            {
                new Autor{ Apellido = "GaRo", Articulos = articulos, Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Estado = true, Imagen = "garo.jpg", Nacimiento = new DateTime(1991, 09, 25), Nombre = "Esteban",
                    Puesto = new Puesto{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro"},
                        Descripcion = "Desarrollador .NET en Financiera Contigo", Nombre = "Desarrollador .NET",
                        Inicio = new DateTime(2016, 01, 18) }, Orden = 1 }
            };
            OpcionMenu[] opcionesP =
            {
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="Acerca De", Orden = 5, Visible = true },
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="Multimedia", Orden = 4, Visible = true },
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="APIs", Orden = 3, Visible = true },
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="Frameworks", Orden = 2, Visible = true },
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="Demostraciones", Orden = 1, Visible = true },
            };
            OpcionMenu[] subOpciones1 =
            {
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="ADO .NET (Clásico)", Orden = 1, Visible = true, Padre = opcionesP[2],
                    Articulo =  articulos[4]},
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="LINQ", Orden = 2, Visible = true, Padre = opcionesP[2],
                    Articulo =  articulos[0]},
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="Microsoft ASP .NET", Orden = 1, Visible = true, Padre = opcionesP[3]},
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="Windows Forms", Orden = 2, Visible = true, Padre = opcionesP[3],
                    Articulo =  articulos[14]},
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="WPF", Orden = 3, Visible = true, Padre = opcionesP[3],
                    Articulo =  articulos[15]},
            };
            OpcionMenu[] subOpciones2 =
            {
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="ASP .NET WEB FORMS", Orden = 2, Visible = true, Padre = subOpciones1[2],
                    Articulo = articulos[11]},
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="ASP .NET MVC", Orden = 1, Visible = true, Padre = subOpciones1[2],
                    Articulo = articulos[12]},
                new OpcionMenu{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion="ASP .NET WEB API", Orden = 3, Visible = true, Padre = subOpciones1[2],
                    Articulo = articulos[13]},
            };
            BotonAviso[] botones =
            {
                new BotonAviso{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Color = "azul_slider", Texto = "Leer"}
            };
            AvisoCarrusel[] avisos =
            {
                new AvisoCarrusel{ Articulo = articulos[13], Boton = botones[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Orden = 1, Visible = true, Contenido = "¡Construye Servicios Web estilo REST con ASP .NET WEB API!"},
                new AvisoCarrusel{ Articulo = articulos[0], Boton = botones[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Orden = 2, Visible = true, Contenido = "¿Qué es LINQ y cómo puedo incluirlo en mis aplicaciones?"},
                new AvisoCarrusel{ Articulo = articulos[5], Boton = botones[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Orden = 3, Visible = true, Contenido = "ORMs y ADO .NET Entity Framework"},
                new AvisoCarrusel{ Articulo = articulos[10], Boton = botones[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Orden = 4, Visible = true, Contenido = "Patrones de diseño en ASP .NET, ¿Cúales son?, ¿Diferencias?, ¿Cómo elegir el mejor?"},
            };
            NoticiaPrincipal[] noticias =
            {
                new NoticiaPrincipal{ Articulo = articulos[9], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "Table Splitting y Entity Spliting en Entity Framework", Imagen = "entity.png",
                    Titulo = "Técnicas de modelado en EF"},
                new NoticiaPrincipal{ Articulo = articulos[5], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "Database First, Model First y Code First", Imagen = "entity.png",
                    Titulo = "Enfoques en Entity Framework"},
                new NoticiaPrincipal{ Articulo = articulos[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "Crea una aplicación MVC + Entity Framework", Imagen = "asp.png",
                    Titulo = "Conociendo una aplicación ASP NET MVC"},
                new NoticiaPrincipal{ Articulo = articulos[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "Usando Entity Framework + MySQL", Imagen = "mysql.png",
                    Titulo = "EF y MySQL"},
                new NoticiaPrincipal{ Articulo = articulos[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "¿Cuál enfoque de Entity Framework usar?", Imagen = "entity.png",
                    Titulo = "Enfoques en EF"},
                new NoticiaPrincipal{ Articulo = articulos[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "Manipulando XMLs con LINQ", Imagen = "entity.png",
                    Titulo = "LINQ to XML"},
                new NoticiaPrincipal{ Articulo = articulos[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "Ventejas y desventajas de WEB FORMS", Imagen = "asp.png",
                    Titulo = "ASP .NET WEB FORMS"},
                new NoticiaPrincipal{ Articulo = articulos[0], Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Descripcion = "Enviando peticiones AJAX a controladores MVC", Imagen = "ajax.png",
                    Titulo = "ASP .NET y AJAX"},
            };
            Multimedia[] multimedia = new Multimedia[]
            {
                new Multimedia{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Articulo = articulos[9], Boton=botones[0], Estado=true, Imagen="fondo.JPG",
                    Informacion="Conoce las ventajas y desventajas del .NET Framework y porqué lo deberías " +
                    "de considerar como tu plataforma de desarrollo preferida.(1)", Orden=0, Titulo="¿Por qué .NET?"},
                new Multimedia{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Articulo = articulos[9], Boton=botones[0], Estado=true, Imagen="fondo2.jpg",
                    Informacion="Conoce las ventajas y desventajas del .NET Framework y porqué lo deberías " +
                    "de considerar como tu plataforma de desarrollo preferida.(3)", Orden=2, Titulo="¿Por qué .NET (3)?"},
                new Multimedia{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                    Articulo = articulos[9], Boton=botones[0], Estado=true, Imagen="fondo2.jpg",
                    Informacion="Conoce las ventajas y desventajas del .NET Framework y porqué lo deberías " +
                    "de considerar como tu plataforma de desarrollo preferida.(2)", Orden=2, Titulo="¿Por qué .NET (4)?"}
            };
            CategoriaPalabraCodigo[] categoriasPalabras = new CategoriaPalabraCodigo[]
            {
                new CategoriaPalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "reservadas"},
                new CategoriaPalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "clases"}
            };
            PalabraCodigo[] palabras = new PalabraCodigo[] 
            {
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "using", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "namespace", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "public", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "Console", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "protected", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "private", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "void", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "override", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "return", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "null", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "get", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "set", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "ref", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "out", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "string", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "int", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "float", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "double", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "short", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "decimal", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "bool", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "true", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "false", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "static", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "object", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "if", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "else", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "for", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "while", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "foreach", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "async", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "await", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "abstract", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "interface", Categoria = categoriasPalabras[0] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "Task", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "DbContext", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "List", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "Dictionary", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "IEnumerable", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "File", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "FileStream", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "StreamReader", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "StreamWritter", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "Controller", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "ApiController", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "HttpResponseMessage", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "HttpRequestMessage", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "MediaTypeFormatter", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "ActionResult", Categoria = categoriasPalabras[1] },
                new PalabraCodigo{ Auditoria = new InfoRegistro { UsuarioCreacion = "estebangaro" },
                Nombre = "PartialViewResult", Categoria = categoriasPalabras[1] }
            };
      
            contexto.CategoriasPalabrasCode.AddRange(categoriasPalabras);
            contexto.PalabrasCode.AddRange(palabras);
            contexto.Articulos.AddRange(articulos);
            contexto.ClasesPersonalizadasCode.AddRange(clasesPersonalizadas);
            contexto.Autores.AddRange(autores);
            contexto.OpcionesMenu.AddRange(opcionesP);
            contexto.OpcionesMenu.AddRange(subOpciones1);
            contexto.OpcionesMenu.AddRange(subOpciones2);
            contexto.BotonesAvisos.AddRange(botones);
            contexto.AvisosCarrusel.AddRange(avisos);
            contexto.NoticiasPrincipales.AddRange(noticias);
            contexto.Multimedia.AddRange(multimedia);

            contexto.SaveChanges();
        }
    }
}