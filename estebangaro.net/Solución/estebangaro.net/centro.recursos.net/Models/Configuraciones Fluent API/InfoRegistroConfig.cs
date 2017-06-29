using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class InfoRegistroConfig:
        ComplexTypeConfiguration<Entity_Framework.InfoRegistro>
    {
        public InfoRegistroConfig()
        {
            Property(_inf => _inf.UsuarioCreacion)
                .HasColumnName("Usuario Creacion")
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(_inf => _inf.UsuarioModificacion)
                .HasColumnName("Usuario Modificacion")
                .IsOptional()
                .IsUnicode()
                .HasMaxLength(50);

            Property(_inf => _inf.Creacion)
                .HasColumnName("Fecha Creacion")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.
                    DatabaseGeneratedOption.Computed);

            Property(_inf => _inf.Modificacion)
                .HasColumnName("Fecha Modificacion")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.
                    DatabaseGeneratedOption.Computed);
        }
    }
}