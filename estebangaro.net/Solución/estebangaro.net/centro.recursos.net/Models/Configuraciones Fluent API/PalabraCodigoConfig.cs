using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class PalabraCodigoConfig: 
        EntityTypeConfiguration<Entity_Framework.PalabraCodigo>
    {
        public PalabraCodigoConfig()
        {
            ToTable("PalabrasCodigo", "Configuracion");

            Property(pc => pc.Nombre)
                .HasColumnName("Descripcion")
                .IsUnicode()
                .HasMaxLength(30)
                .IsRequired();

            // Se define asociación con CategoriaPalabraCodigo.
            HasRequired(pc => pc.Categoria)
                .WithMany(cat => cat.Palabras)
                .HasForeignKey(pc => pc.CategoriaId);
        }
    }
}