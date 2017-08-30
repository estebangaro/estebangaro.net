using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class CategoriaPalabraCodigoConfig: 
        EntityTypeConfiguration<Entity_Framework.CategoriaPalabraCodigo>
    {
        public CategoriaPalabraCodigoConfig()
        {
            ToTable("CategoriasPalabrasCodigo", "Configuracion");

            Property(cpc => cpc.Nombre)
                .HasColumnName("Descripcion")
                .IsUnicode()
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}