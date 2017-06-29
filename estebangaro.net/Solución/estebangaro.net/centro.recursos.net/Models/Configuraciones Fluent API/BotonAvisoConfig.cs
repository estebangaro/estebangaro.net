using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class BotonAvisoConfig: 
        EntityTypeConfiguration<Entity_Framework.BotonAviso>
    {
        public BotonAvisoConfig()
        {
            ToTable("Botones", "Slider");

            Property(_btn => _btn.Texto)
                .HasColumnName("Etiqueta")
                .IsUnicode()
                .HasMaxLength(30)
                .IsRequired();

            Property(_btn => _btn.Color)
                .HasColumnName("Fondo")
                .IsUnicode()
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}