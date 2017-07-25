using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class AutorConfig:
        EntityTypeConfiguration<Entity_Framework.Autor>
    {
        public AutorConfig()
        {
            ToTable("Autores", "General");

            Property(notiP => notiP.Nombre)
                .IsUnicode()
                .HasMaxLength(60)
                .IsRequired();

            Property(notiP => notiP.Apellido)
                .IsUnicode()
                .HasMaxLength(60)
                .IsRequired();

            Property(notiP => notiP.ApellidoM)
                .IsUnicode()
                .HasMaxLength(60)
                .IsOptional();

            Property(a => a.Nacimiento)
                .IsRequired()
                .HasColumnType("date");

            Property(a => a.Imagen)
                .HasColumnName("Imagen")
                .IsUnicode()
                .HasMaxLength(255)
                .IsOptional();

            /* Asociaciones con Puesto y Articulos se describen en las respectivas configuraciones */
        }

    }
}