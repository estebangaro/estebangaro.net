using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class ArticuloConfig: 
        EntityTypeConfiguration<Entity_Framework.Articulo>
    {
        public ArticuloConfig()
        {
            ToTable("Articulos", "Vista");

            HasKey(a => a.URI).
                Property(a => a.URI).
                HasMaxLength(200).
                IsUnicode();
            Property(a => a.Titulo)
                .HasColumnName("Titulo")
                .IsUnicode()
                .HasMaxLength(80)
                .IsRequired();
            Property(a => a.Localizacion)
                .HasColumnName("Ciudad")
                .IsUnicode()
                .HasMaxLength(80)
                .IsRequired();

            HasMany(a => a.Autores).
                WithMany(au => au.Articulos).
                Map(a_au =>
                {
                    a_au.MapLeftKey("ArticuloId");
                    a_au.MapRightKey("AutorId");
                    a_au.ToTable("ArticulosAutores");
                });

            HasMany(a => a.Comentarios).
                WithRequired(co => co.Articulo).
            HasForeignKey(co => co.IdArticulo);

        }
    }
}