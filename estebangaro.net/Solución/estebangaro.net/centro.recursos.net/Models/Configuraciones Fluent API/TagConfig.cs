using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class TagConfig: EntityTypeConfiguration<Entity_Framework.Tag>
    {
        public TagConfig()
        {
            ToTable("Etiquetas", "Vista");

            HasKey(tag => tag.Etiqueta)
                .Property(tag => tag.Etiqueta)
                .IsUnicode()
                .HasMaxLength(120);

            Property(tag => tag.Descripcion)
                .IsUnicode()
                .HasMaxLength(60)
                .IsOptional();

            Property(tag => tag.Categoria)
                .IsUnicode()
                .HasMaxLength(60)
                .IsOptional();

            HasMany(tag => tag.Articulos)
                .WithMany(articulo => articulo.Etiquetas)
                .Map(config =>
                    config.MapLeftKey("ArticuloId")
                        .MapRightKey("TagId")
                        .ToTable("ArticulosEtiquetas"));
        }
    }
}