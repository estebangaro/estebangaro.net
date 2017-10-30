using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class ClienteConfig: EntityTypeConfiguration<Entity_Framework.Cliente>
    {
        public ClienteConfig()
        {
            ToTable("Clientes", "General")
                .HasKey(cliente => cliente.Email)
                .Property(cliente => cliente.Email)
                .HasColumnName("IdCliente")
                .IsUnicode()
                .HasMaxLength(255);

            Property(cliente => cliente.Nombre)
                .IsUnicode()
                .IsRequired()
                .HasMaxLength(60);

            Property(cliente => cliente.Avatar)
                .IsUnicode()
                .HasColumnName("Foto")
                .HasMaxLength(60);

            HasMany(cliente => cliente.Comentarios)
                .WithRequired(comentario => comentario.Cliente)
                .HasForeignKey(comentario => comentario.Email);
        }
    }
}