using centro.recursos.net.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class MultimediaConfig: 
        EntityTypeConfiguration<Multimedia>
    {
        public MultimediaConfig()
        {
            ToTable("Multimedia", "SeccionPrincipal")
                .Property(mult => mult.Informacion)
                .HasColumnName("Contenido")
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(255);

            Property(mult => mult.Titulo)
                .HasColumnName("TituloMultimedia")
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(50);

            Property(mult => mult.Imagen)
                .HasColumnName("UriImagen")
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(200);

            Property(mult => mult.Orden)
                .HasColumnName("Prioridad")
                .IsRequired();

            Property(mult => mult.Estado)
                .HasColumnName("Visible")
                .IsRequired();

            // Asociaciones
            HasRequired(mult => mult.Articulo)
                .WithMany(art => art.Multimedia)
                .HasForeignKey(mult => mult.URI);

            HasRequired(mult => mult.Boton)
                .WithMany(btn => btn.Multimedia)
                .HasForeignKey(mult => mult.BotonId);
        }
    }
}