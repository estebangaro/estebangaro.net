using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class NoticiaPrincipalConfig: 
        EntityTypeConfiguration<Entity_Framework.NoticiaPrincipal>
    {
        public NoticiaPrincipalConfig()
        {
            ToTable("Noticias", "SeccionPrincipal");

            Property(notiP => notiP.Titulo)
                .IsUnicode()
                .HasMaxLength(60)
                .IsRequired();
            Property(notiP => notiP.Imagen)
                .IsUnicode()
                .HasMaxLength(200)
                .IsRequired();
            Property(notiP => notiP.Descripcion)
                .IsUnicode()
                .HasMaxLength(130)
                .IsRequired();
            Property(notiP => notiP.URI)
                .IsUnicode()
                .HasMaxLength(200)
                .IsRequired();
            HasRequired(notiP => notiP.Articulo)
                .WithMany(ar => ar.Noticias)
                .HasForeignKey(notiP => notiP.URI);
        }
    }
}
