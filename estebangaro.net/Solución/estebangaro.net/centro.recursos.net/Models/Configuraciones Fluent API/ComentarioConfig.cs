using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Configuraciones_Fluent_API
{
    public class ComentarioConfig:
        EntityTypeConfiguration<Entity_Framework.Comentario>
    {
        public ComentarioConfig()
        {
            ToTable("Comentarios", "Vista");

            Property(_op => _op.Contenido)
                .HasColumnName("ContenidoComentario")
                .IsUnicode()
                .HasMaxLength(500)
                .IsRequired();

            HasMany(com => com.Comentarios).
                WithOptional(com => com.ComentarioPadre).
                HasForeignKey(com => com.IdComentarioP);
            
            /*Asociación con Artículo se define en la configuración "padre"*/
        }
    }
}