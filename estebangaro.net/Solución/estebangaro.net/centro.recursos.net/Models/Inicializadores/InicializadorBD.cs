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
        }
    }
}