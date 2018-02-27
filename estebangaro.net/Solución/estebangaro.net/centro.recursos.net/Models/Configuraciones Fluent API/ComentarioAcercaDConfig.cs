using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class ComentarioAcercaDConfig: EntityTypeConfiguration<Entity_Framework.ComentarioAcercaD>
    {
        public ComentarioAcercaDConfig()
        {
            ToTable("ComentariosAcercaD", "General");

            HasKey<int>(coment => coment.NumeroComentario)
                .Property(coment => coment.NumeroComentario)
                .HasColumnName("Id");
            Property(coment => coment.Nombre)
                .HasColumnName("Nombre visitante")
                .IsUnicode()
                .HasMaxLength(60)
                .IsRequired();
            Property(coment => coment.Email)
                .HasColumnName("Email visitante")
                .IsUnicode()
                .HasMaxLength(255)
                .IsRequired();
            Property(coment => coment.Ciudad)
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();
            Property(coment => coment.Asuntomsj)
                .HasColumnName("Asunto")
                .IsUnicode()
                .HasMaxLength(30)
                .IsRequired();
            Property(coment => coment.Contenidomsj)
                .HasColumnName("Contenido")
                .IsUnicode()
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}