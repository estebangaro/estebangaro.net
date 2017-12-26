namespace centro.recursos.net.Models.Entity_Framework
{
    public class RedSocial
    {
        public string URI { get; set; }
        public string Icono { get; set; }
        public string Descripcion { get; set; }
        public bool Visible { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public int AutorId { get; set; }
        public virtual Autor Autor { get; set; }
    }
}