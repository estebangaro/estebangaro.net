namespace centro.recursos.net.Models.Entity_Framework
{
    using centro.recursos.net.Models.Configuraciones_Fluent_API;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GaroNETDbContexto : DbContext
    {
        public GaroNETDbContexto()
            : base("name=GaroNETDbContexto") { }

        public GaroNETDbContexto(string nombreConexion)
            : base($"name={nombreConexion}") { }

        public virtual DbSet<OpcionMenu> OpcionesMenu { get; set; }
        public virtual DbSet<BotonAviso> BotonesAvisos { get; set; }
        public virtual DbSet<AvisoCarrusel> AvisosCarrusel { get; set; }
        public virtual DbSet<NoticiaPrincipal> NoticiasPrincipales { get; set; }
        public virtual DbSet<Articulo> Articulos { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Puesto> Puestos { get; set; }
        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<Multimedia> Multimedia { get; set; }
        public virtual DbSet<PalabraCodigo> PalabrasCode { get; set; }
        public virtual DbSet<CategoriaPalabraCodigo> CategoriasPalabrasCode { get; set; }
        public virtual DbSet<ClasePersonalizada> ClasesPersonalizadasCode { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InfoRegistroConfig())
                .Add(new OpcionMenuConfig())
                .Add(new BotonAvisoConfig())
                .Add(new AvisoCarruselConfig())
                .Add(new NoticiaPrincipalConfig())
                .Add(new ArticuloConfig())
                .Add(new ComentarioConfig())
                .Add(new PuestoConfig())
                .Add(new AutorConfig())
                .Add(new MultimediaConfig())
                .Add(new PalabraCodigoConfig())
                .Add(new CategoriaPalabraCodigoConfig())
                .Add(new ClasePersonalizadaConfig());
        }
    }
}