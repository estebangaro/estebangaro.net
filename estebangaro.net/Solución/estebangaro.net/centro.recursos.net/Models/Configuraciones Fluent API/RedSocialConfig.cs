using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class RedSocialConfig: EntityTypeConfiguration<Entity_Framework.RedSocial>
    {
        public RedSocialConfig()
        {
            ToTable("RedesSociales", "General");

            HasKey(red => red.URI)
                .Property(red => red.URI)
                .HasColumnName("Link")
                .IsUnicode()
                .HasMaxLength(200);

            Property(a => a.Icono)
                .HasColumnName("Icono")
                .IsUnicode()
                .HasMaxLength(255)
                .IsRequired();

            Property(a => a.Descripcion)
                .HasColumnName("Descripcion")
                .IsUnicode()
                .HasMaxLength(100)
                .IsOptional();

            Property(red => red.Visible)
                .HasColumnName("Estado")
                .IsRequired();

            HasRequired(red => red.Autor)
                .WithMany(autor => autor.RedesSociales)
                .HasForeignKey(red => red.AutorId);
        }
    }
}