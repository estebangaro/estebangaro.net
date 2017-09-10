using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class ClasePersonalizadaConfig: 
        EntityTypeConfiguration<Entity_Framework.ClasePersonalizada>
    {
        public ClasePersonalizadaConfig()
        {
            ToTable("ClasesPersonalizadas", "Configuracion");

            Property(pc => pc.Nombre)
                .HasColumnName("Descripcion")
                .IsUnicode()
                .HasMaxLength(60)
                .IsRequired();

            // Se define asociación con Articulo.
            HasRequired(pc => pc.Articulo)
                .WithMany(cat => cat.Clases)
                .HasForeignKey(pc => pc.ArticuloId);
        }
    }
}