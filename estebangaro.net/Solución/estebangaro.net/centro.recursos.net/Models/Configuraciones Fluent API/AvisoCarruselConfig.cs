using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class AvisoCarruselConfig:
        EntityTypeConfiguration<Entity_Framework.AvisoCarrusel>
    {
        public AvisoCarruselConfig()
        {
            ToTable("Avisos", "Slider");

            Property(_av => _av.Contenido)
                .HasColumnName("Contenido Aviso")
                .IsUnicode()
                .HasMaxLength(130)
                .IsRequired();

            Property(_av => _av.URI)
               .HasColumnName("URL")
               .IsUnicode()
               .HasMaxLength(200)
               .IsRequired();

            Property(_av => _av.Visible)
                .HasColumnName("Es Visible")
                .IsRequired();

            HasRequired(_av => _av.Boton)
                .WithMany(_btn => _btn.Avisos)
                .HasForeignKey(_av => _av.BotonId);
        }
    }
}