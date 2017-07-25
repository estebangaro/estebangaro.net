using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class PuestoConfig:
        EntityTypeConfiguration<Entity_Framework.Puesto>
    {
        public PuestoConfig()
        {
            ToTable("Puestos", "General");

            Property(a => a.Nombre)
                .HasColumnName("Nombre")
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();

            Property(a => a.Descripcion)
                .HasColumnName("Descripcion")
                .IsUnicode()
                .HasMaxLength(80)
                .IsRequired();

            Property(a => a.Inicio)
                .IsRequired()
                .HasColumnType("date");

            HasMany(p => p.Autores).
                WithRequired(au => au.Puesto).
                HasForeignKey(au => au.IdPuesto);

        }
    }

}