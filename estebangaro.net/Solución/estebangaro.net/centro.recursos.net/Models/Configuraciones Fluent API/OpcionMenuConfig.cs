using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;


namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class OpcionMenuConfig: 
        EntityTypeConfiguration<Entity_Framework.OpcionMenu>
    {
        public OpcionMenuConfig()
        {
            ToTable("Opciones", "Menu");

            Property(_op => _op.Descripcion)
                .HasColumnName("Nombre")
                .IsUnicode()
                .HasMaxLength(30)
                .IsRequired();

            Property(_op => _op.URI)
               .HasColumnName("URL")
               .IsUnicode()
               .HasMaxLength(200)
               .IsOptional();

            Property(_op => _op.Visible)
                .HasColumnName("Es Visible")
                .IsRequired();

            HasMany(_op => _op.Opciones)
                .WithOptional(_op => _op.Padre)
                .HasForeignKey(_op => _op.MenuPadre);
        }
    }
}